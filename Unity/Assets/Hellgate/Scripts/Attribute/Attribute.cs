﻿//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//					Hellgate Framework
// Copyright © Uniqtem Co., Ltd.
//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
using UnityEngine;
using System;

namespace Hellgate
{
    /// <summary>
    /// Data constraints.
    /// </summary>
    public enum DataConstraints
    {
        // NOT NULL
        NOTNULL,
        // PRIMARY KEY
        PK,
        // FOREIGN KEY
        FK,
        // AUTOINCREMENT
        AI,
        // UNIQUE
        UNIQUE
    }

    public class AttributeMappingConfig<T>
    {
        /// <summary>
        /// T.
        /// </summary>
        public T t;
        /// <summary>
        /// The name.
        /// </summary>
        public string name;
        /// <summary>
        /// The type.
        /// </summary>
        public Type type;
    }

    public class TableAttribute : Attribute
    {
        private bool tableAutoGenerated;
        private string tableName;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Hellgate.TableAttribute"/> table auto generated.
        /// </summary>
        /// <value><c>true</c> if table auto generated; otherwise, <c>false</c>.</value>
        public bool TableAutoGenerated {
            get {
                return tableAutoGenerated;
            }
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName {
            get {
                return tableName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.TableAttribute"/> class.
        /// </summary>
        public TableAttribute ()
        {
            this.tableName = "";
            tableAutoGenerated = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.TableAttribute"/> class.
        /// </summary>
        /// <param name="tableAutoGenerated">If set to <c>true</c> table auto generated.</param>
        public TableAttribute (bool tableAutoGenerated)
        {
            tableName = "";
            this.tableAutoGenerated = tableAutoGenerated;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.TableAttribute"/> class.
        /// </summary>
        /// <param name="tableName">Table name.</param>
        /// <param name="tableAutoGenerated">If set to <c>true</c> table auto generated.</param>
        public TableAttribute (string tableName, bool tableAutoGenerated = false)
        {
            this.tableName = tableName;
            this.tableAutoGenerated = tableAutoGenerated;
        }
    }

    [AttributeUsage (AttributeTargets.Field)]
    public class ColumnAttribute : Attribute
    {
        private DataConstraints[] constraints;
        private string type = "";
        private Type key = null;
        private string value = "";
        private bool isConstraints = true;

        /// <summary>
        /// Gets the constraints.
        /// </summary>
        /// <value>The constraints.</value>
        public DataConstraints[] Constraints {
            get {
                return constraints;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type {
            get {
                return type;
            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public Type Key {
            get {
                return key;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value {
            get {
                return Util.ConvertCamelToUnderscore (this.value);
            }
        }

        /// <summary>
        /// Checks the constraints.
        /// </summary>
        /// <returns><c>true</c>, if constraints was checked, <c>false</c> otherwise.</returns>
        /// <param name="constraints">Constraints.</param>
        public bool CheckConstraints (DataConstraints constraints)
        {
            if (!isConstraints) {
                return false;
            }

            if (Array.FindIndex (this.constraints, c => c == constraints) < 0) {
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        public ColumnAttribute ()
        {
            isConstraints = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="constraints">Constraints.</param>
        /// <param name="key">Key. If constraints [FK] table name</param>
        /// <param name="value">Value. If constraints [FK] column name</param>
        public ColumnAttribute (DataConstraints constraints, Type key = null, string value = "")
        {
            this.constraints = new DataConstraints[] { constraints };
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="constraints">Constraints.</param>
        public ColumnAttribute (DataConstraints[] constraints)
        {
            this.constraints = constraints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        public ColumnAttribute (string type)
        {
            isConstraints = false;
            this.type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="constraints">Constraints.</param>
        /// <param name="key">Key. If constraints [FK] table name</param>
        /// <param name="value">Value. If constraints [FK] column name</param>
        public ColumnAttribute (string type, DataConstraints constraints, Type key = null, string value = "")
        {
            this.type = type;
            this.constraints = new DataConstraints[] { constraints };
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="constraints">Constraints.</param>
        public ColumnAttribute (string type, DataConstraints[] constraints)
        {
            this.type = type;
            this.constraints = constraints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hellgate.ColumnAttribute"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public ColumnAttribute (Type key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [AttributeUsage (AttributeTargets.Field)]
    public class JoinAttribute : Attribute
    {
        private SqliteJoinType type = SqliteJoinType.NONE;

        public SqliteJoinType Type {
            get {
                return type;
            }
        }

        public JoinAttribute (SqliteJoinType type)
        {
            this.type = type;
        }
    }

    public class ExcelAttribute : Attribute
    {
        private string sheetName;
        private string createFileName;
        private bool indexFlag;

        public string SheetName {
            get {
                return sheetName;
            }
        }

        public string CreateFileName {
            get {
                return createFileName;
            }
        }

        public bool IndexFlag {
            get {
                return indexFlag;
            }
        }

        public ExcelAttribute (string sheetName, string createFileName = "", bool indexFlag = false)
        {
            this.sheetName = sheetName;
            this.createFileName = createFileName;
            this.indexFlag = indexFlag;
        }
    }

    public class IgnoreAttribute : Attribute
    {
    }
}
