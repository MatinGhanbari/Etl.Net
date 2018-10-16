﻿using Paillave.Etl;
using Paillave.Etl.Core.Streams;
using Paillave.Etl.SqlServer.StreamNodes;
using Paillave.Etl.SqlServer.ValuesProviders;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Paillave.Etl.SqlServer
{
    public static class SqlServerEx
    {
        public static IStream<TOut> CrossApplySqlServerQuery<TIn, TOut>(this IStream<TIn> stream, string name, ISingleStream<SqlConnection> sqlConnection, string sqlQuery, bool noParallelisation = false)
        {
            return stream.CrossApply(name, sqlConnection, new SqlCommandValueProvider<TIn, TOut>(new SqlCommandValueProviderArgs<TIn, TOut>()
            {
                SqlQuery = sqlQuery,
                NoParallelisation = noParallelisation
            }));
        }
        public static IStream<TIn> ThroughSqlServer<TIn>(this IStream<TIn> stream, string name, ISingleStream<SqlConnection> sqlConnection, string sqlQuery)
            where TIn : class
        {
            return new ThroughSqlCommandStreamNode<TIn, IStream<TIn>>(name, new ThroughSqlCommandArgs<TIn, IStream<TIn>>
            {
                SourceStream = stream,
                SqlConnectionStream = sqlConnection,
                SqlQuery = sqlQuery
            }).Output;
        }
    }
}
