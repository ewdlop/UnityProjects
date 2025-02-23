<?xml version="1.0" encoding="utf-8"?>
<Type Name="DataColumnMapping" FullName="System.Data.Common.DataColumnMapping">
  <TypeSignature Language="C#" Maintainer="auto" Value="public sealed class DataColumnMapping : MarshalByRefObject, ICloneable, System.Data.IColumnMapping" />
  <AssemblyInfo>
    <AssemblyName>System.Data</AssemblyName>
    <AssemblyPublicKey>[00 00 00 00 00 00 00 00 04 00 00 00 00 00 00 00]</AssemblyPublicKey>
    <AssemblyVersion>1.0.3300.0</AssemblyVersion>
    <AssemblyVersion>1.0.5000.0</AssemblyVersion>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <ThreadSafetyStatement>Gtk# is thread aware, but not thread safe; See the &lt;link location="node:gtk-sharp/programming/threads"&gt;Gtk# Thread Programming&lt;/link&gt; for details.</ThreadSafetyStatement>
  <Base>
    <BaseTypeName>System.MarshalByRefObject</BaseTypeName>
  </Base>
  <Interfaces>
    <Interface>
      <InterfaceName>System.Data.IColumnMapping</InterfaceName>
    </Interface>
    <Interface>
      <InterfaceName>System.ICloneable</InterfaceName>
    </Interface>
  </Interfaces>
  <Attributes>
    <Attribute>
      <AttributeName>System.ComponentModel.TypeConverter("System.Data.Common.DataColumnMapping+DataColumnMappingConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")</AttributeName>
    </Attribute>
  </Attributes>
  <Docs>
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>A <see cref="T:System.Data.Common.DataColumnMapping" /> enables you to use column names in a <see cref="T:System.Data.DataTable" /> that are different from those in the data source. The DataAdapter uses the mapping to match the columns when the tables in the <see cref="T:System.Data.DataSet" /> or data source are updated. For more information, see <format type="text/html"><a href="D023260A-A66A-4C39-B8F4-090CD130E730">[&lt;topic://cpconsettingupdatatabledatacolumnmappings&gt;]</a></format>.</para>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Contains a generic column mapping for an object that inherits from <see cref="T:System.Data.Common.DataAdapter" />. This class cannot be inherited.</para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="public DataColumnMapping ();" />
      <MemberType>Constructor</MemberType>
      <ReturnValue />
      <Parameters />
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class.</para>
        </summary>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="public DataColumnMapping (string sourceColumn, string dataSetColumn);" />
      <MemberType>Constructor</MemberType>
      <ReturnValue />
      <Parameters>
        <Parameter Name="sourceColumn" Type="System.String" />
        <Parameter Name="dataSetColumn" Type="System.String" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class with the specified source column name and <see cref="T:System.Data.DataSet" /> column name to map to.</para>
        </summary>
        <param name="sourceColumn">
          <attribution license="cc4" from="Microsoft" modified="false" />The case-sensitive column name from a data source. </param>
        <param name="dataSetColumn">
          <attribution license="cc4" from="Microsoft" modified="false" />The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="DataSetColumn">
      <MemberSignature Language="C#" Value="public string DataSetColumn { set; get; }" />
      <MemberType>Property</MemberType>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Parameters>
      </Parameters>
      <Docs>
        <value>To be added: an object of type 'string'</value>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</para>
        </summary>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.ComponentModel.DefaultValue("")</AttributeName>
        </Attribute>
      </Attributes>
    </Member>
    <Member MemberName="GetDataColumnBySchemaAction">
      <MemberSignature Language="C#" Value="public System.Data.DataColumn GetDataColumnBySchemaAction (System.Data.DataTable dataTable, Type dataType, System.Data.MissingSchemaAction schemaAction);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Data.DataColumn</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="dataTable" Type="System.Data.DataTable" />
        <Parameter Name="dataType" Type="System.Type" />
        <Parameter Name="schemaAction" Type="System.Data.MissingSchemaAction" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets a <see cref="T:System.Data.DataColumn" /> from the given <see cref="T:System.Data.DataTable" /> using the <see cref="T:System.Data.MissingSchemaAction" /> and the <see cref="P:System.Data.Common.DataColumnMapping.DataSetColumn" /> property.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A data column.</para>
        </returns>
        <param name="dataTable">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.Data.DataTable" /> to get the column from.</param>
        <param name="dataType">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.Type" /> of the data column.</param>
        <param name="schemaAction">
          <attribution license="cc4" from="Microsoft" modified="false" />One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)</AttributeName>
        </Attribute>
      </Attributes>
    </Member>
    <Member MemberName="GetDataColumnBySchemaAction">
      <MemberSignature Language="C#" Value="public static System.Data.DataColumn GetDataColumnBySchemaAction (string sourceColumn, string dataSetColumn, System.Data.DataTable dataTable, Type dataType, System.Data.MissingSchemaAction schemaAction);" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.Data.DataColumn</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="sourceColumn" Type="System.String" />
        <Parameter Name="dataSetColumn" Type="System.String" />
        <Parameter Name="dataTable" Type="System.Data.DataTable" />
        <Parameter Name="dataType" Type="System.Type" />
        <Parameter Name="schemaAction" Type="System.Data.MissingSchemaAction" />
      </Parameters>
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A static version of <see cref="M:System.Data.Common.DataColumnMapping.GetDataColumnBySchemaAction(System.Data.DataTable,System.Type,System.Data.MissingSchemaAction)" /> that can be called without instantiating a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A <see cref="T:System.Data.DataColumn" /> object.</para>
        </returns>
        <param name="sourceColumn">
          <attribution license="cc4" from="Microsoft" modified="false" />The case-sensitive column name from a data source. </param>
        <param name="dataSetColumn">
          <attribution license="cc4" from="Microsoft" modified="false" />The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
        <param name="dataTable">
          <attribution license="cc4" from="Microsoft" modified="false" />An instance of <see cref="T:System.Data.DataTable" />.</param>
        <param name="dataType">
          <attribution license="cc4" from="Microsoft" modified="false" />The data type for the column being mapped.</param>
        <param name="schemaAction">
          <attribution license="cc4" from="Microsoft" modified="false" />Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
      </Docs>
    </Member>
    <Member MemberName="SourceColumn">
      <MemberSignature Language="C#" Value="public string SourceColumn { set; get; }" />
      <MemberType>Property</MemberType>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Parameters>
      </Parameters>
      <Docs>
        <value>To be added: an object of type 'string'</value>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</para>
        </summary>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.ComponentModel.DefaultValue("")</AttributeName>
        </Attribute>
      </Attributes>
    </Member>
    <Member MemberName="System.ICloneable.Clone">
      <MemberSignature Language="C#" Value="object ICloneable.Clone ();" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Object</ReturnType>
      </ReturnValue>
      <Parameters />
      <Docs>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>This member is an explicit interface member implementation. It can be used only when the <see cref="T:System.Data.SqlClient.SqlCommand" /> instance is cast to an <see cref="T:System.ICloneable" /> interface.</para>
          <para>For more information, see <see cref="M:System.ICloneable.Clone" />.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a new object that is a copy of the current instance.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A copy of the current object.</para>
        </returns>
      </Docs>
    </Member>
    <Member MemberName="ToString">
      <MemberSignature Language="C#" Value="public override string ToString ();" />
      <MemberType>Method</MemberType>