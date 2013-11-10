﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace Library.DataAccess.DBInterop
{
    public class ConnectionProvider
    {
        public string ConnectionString {
            get;
            protected set;
        }

        public ConnectionProvider(string connectionString) {
            ConnectionString = connectionString;
        }

        public OracleConnection CreateConnection() {
            return new OracleConnection(ConnectionString);
        }

        public virtual void ExecuteNonQuery(OracleCommand command) {
            using (var connection = CreateConnection()) {
                connection.Open();
                using (command) {
                    using (var transaction = connection.BeginTransaction()) {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        try {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        } catch (Exception exc) {
                            transaction.Rollback();
                            throw exc;
                        }
                    }
                }
            }
        }

        public virtual void ExecuteNonQueries(IEnumerable<OracleCommand> commands) {
            using (var connection = CreateConnection()) {
                connection.Open();
                using (var transaction = connection.BeginTransaction()) {
                    try {
                        foreach (var command in commands) {
                            command.Connection = connection;
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                    } catch (Exception exc) {
                        transaction.Rollback();
                        throw exc;
                    }
                    transaction.Commit();
                }
            }
        }

        public virtual DataSet ExecuteTable(OracleCommand command) {
            using (var connection = CreateConnection()) {
                connection.Open();
                using (command) {
                    command.Connection = connection;
                    using (var adapter = new OracleDataAdapter(command)) {
                        var set = new DataSet();
                        adapter.Fill(set);
                        return set;
                    }
                }
            }
        }

        public virtual object ExecuteScalar(OracleCommand command) {
            using (var connection = CreateConnection()) {
                connection.Open();
                using (command) {
                    command.Connection = connection;
                    return command.ExecuteScalar();
                }
            }
        }


    }
}
