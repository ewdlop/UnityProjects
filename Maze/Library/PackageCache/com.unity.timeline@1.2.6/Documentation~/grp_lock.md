using UnityEngine;
using System.Collections;

#pragma warning disable 0649 // Disabled warnings.

namespace TMPro
{
    
    [System.Serializable]
    public class TMP_Style
    {
        // PUBLIC PROPERTIES

        /// <summary>
        /// The name identifying this style. ex. <style="name">.
        /// </summary>
        public string name
        { get { return m_Name; } set { if (value != m_Name) m_Name = value; } }
       
        /// <summary>
        /// The hash code corresponding to the name of this style.
        /// </summary>
        public int hashCode
        { get { return m_HashCode; } set { if (value != m_HashCode) m_HashCode = value; } }

        /// <summary>
        /// The initial definition of the style. ex. <b> <u>.
        /// </summary>
        public string styleOpeningDefinition
        { get { return m_OpeningDefinition; } }

        /// <summary>
        /// The closing definition of the style. ex. </b> </u>.
        /// </summary>
        public string styleClosingDefinition
        { get { return m_ClosingDefinition; } }


        public int[] styleOpeningTagArray
        { get { return m_OpeningTagArray; } }


        public int[] styleClosingTagArray
        { get { return m_ClosingTagArray; } }

       
        // PRIVATE FIELDS
        [SerializeField]
        private string m_Name;

        [SerializeField]
        private int m_HashCode;

        [SerializeField]
        private string m_OpeningDefinition;

      