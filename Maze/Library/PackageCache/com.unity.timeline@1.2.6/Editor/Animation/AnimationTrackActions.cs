//---------------------------------------------------------------------
// <copyright file="Domain.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// @owner Microsoft
// @backupOwner Microsoft
//---------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
    using CellConstantSet = Set<Constant>;

    // A set of cell constants -- to keep track of a cell constant's domain
    // values. It encapsulates the notions of NULL, NOT NULL and can be
    // enhanced in the future with more functionality
    // To represent "infinite" domains such as integer, a special constant CellConstant.NotNull is used. 
    // For example: domain of System.Boolean is {true, false}, domain of
    // (nullable) System.Int32 property is {Null, NotNull}.
    internal class Domain : InternalBase
    {

        #region Constructors
        // effects: Creates an "fully-done" set with no values -- possibleDiscreteValues are the values
        // that this domain can take
        internal Domain(Constant value, IEnumerable<Constant> possibleDiscreteValues) :
            this(new Constant[] { value }, possibleDiscreteValues)
        {
        }

        // effects: Creates a domain populated using values -- possibleValues
        // are all possible values that this can take
        internal Domain(IEnumerable<Constant> values,
                                    IEnumerable<Constant> possibleDiscreteValues)
        {
            // Note that the values can contain both null and not null
            Debug.Assert(values != null);
            Debug.Assert(possibleDiscreteValues != null);
            // Determine the possibleValues first and then create the negatedConstant
            m_possibleValues = DeterminePossibleValues(values, possibleDiscreteValues);

            // Now we need to make sure that m_domain is correct. if "values" (v) already has
            // the negated stuff, we need to make sure it is in conformance
            // with what m_possibleValues (p) has

            // For NOT --> Add all constants into d that are present in p but
            // not in the NOT
            // v = 1, NOT(1, 2);                            p = 1, 2, 3         => d = 1, NOT(1, 2, 3), 3
            // v = 1, 2, NOT(1);                            p = 1, 2, 4         => d = 1, 2, 4, NOT(1, 2, 4)
            // v = 1, 2, NOT(1, 2, 4), NOT(1, 2, 4, 5);     p = 1, 2, 4, 5, 6   => d = 1, 2, 5, 6, NOT(1, 2, 4, 5, 6)

            // NotNull works naturally now. If possibleValues has (1, 2, NULL) and values has NOT(NULL), add 1, 2 to m_domain

            m_domain = ExpandNegationsInDomain(values, m_possibleValues);
            AssertInvariant();
        }

        // effects: Creates a copy of the set "domain"
        internal Domain(Domain domain)
        {
            m_domain = new Set<Constant>(domain.m_domain, Constant.EqualityComparer);
            m_possibleValues = new Set<Constant>(domain.m_possibleValues, Constant.EqualityComparer);
            AssertInvariant();
        }
        #endregion

        #region Fields
        // The set of values in the cell constant domain
        private CellConstantSet m_domain; // e.g., 1, 2, NULL, NOT(1, 2, NULL)
        private CellConstantSet m_possibleValues; // e.g., 1, 2, NULL, Undefined
        // Invariant: m_domain is a subset of m_possibleValues except for a
        // negated constant
        #endregion

        #region Properties
        // effects: Returns all the possible values that this can contain (including the negated constants)
        internal IEnumerable<Constant> AllPossibleValues
        {
            get { return AllPossibleValuesInternal; }
        }

        // effects: Returns all the possible values that this can contain (including the negated constants)
        private Set<Constant> AllPossibleValuesInternal
        {
            get
            {
                NegatedConstant negatedPossibleValue = new NegatedConstant(m_possibleValues);
                return m_possibleValues.Union(new Constant[] { negatedPossibleValue });
            }
        }

        // effects: Returns the number of constants in this (including a negated constant)
        internal int Count
        {
            get { return m_domain.Count; }
        }

        /// <summary>
        /// Yields the set of all values in the domain.
        /// </summary>
        internal IEnumerable<Constant> Values
        {
            get { return m_domain; }
        }
        #endregion

        #region Static Helper Methods to create cell constant sets from metadata
        // effects: Given a member, determines all possible values that can be created from Metadata
        internal static CellConstantSet DeriveDomainFromMemberPath(MemberPath memberPath, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
        {
            CellConstantSet domain = DeriveDomainFromType(memberPath.EdmType, edmItemCollection, leaveDomainUnbounded);
            if (memberPath.IsNullable)
            {
                domain.Add(Constant.Null);
            }
            ret