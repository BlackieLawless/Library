﻿using Library.DataAccess.DBInterop.Queries.Abstract;
using Library.DataContracts.Concrete;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DBInterop.Queries.Concrete
{
    public class GetRequestHeadersQuery : TableQuery<RequestHeader>
    {
        const string Query = @"select
                                    distinct r.request_id, r.request_card_id, r.request_create_date, (case when ra.c is null then 0 else 1 end) as has_approved, (case when rr.c is null then 0 else 1 end) as has_rejected
                                  from(
                                    select 
                                      distinct request_id, request_card_id, request_create_date, request_book_id
                                    from request
                                  ) r
                                  left join (
                                    select ra.request_approved_request_id, count(ra.request_approved_request_id) as c from request_approved ra group by ra.request_approved_request_id
                                  ) ra on r.request_id = ra.request_approved_request_id
                                  left join (
                                    select rr.request_rejected_request_id, count(rr.request_rejected_request_id) as c from request_rejected rr group by rr.request_rejected_request_id
                                  ) rr on r.request_id = rr.request_rejected_request_id
                                  inner join book b on r.request_book_id = b.book_id
                                  where (:card_id is null or r.request_card_id = :card_id) and (:book_name is null or lower(b.book_name) like '%'||lower(:book_name)||'%')
                                  order by r.request_card_id, r.request_create_date desc";

        public GetRequestHeadersQuery(ConnectionProvider provider)
            : base(provider) {

        }

        public Card Card {
            get;
            set;
        }

        public string Search {
            get;
            set;
        }

        public override RequestHeader Read(DataRow row) {
            return new RequestHeader() {
                Id = Convert.ToInt32(row.Field<decimal>("request_id")),
                Card = new Card() {
                    Id = Convert.ToInt32(row.Field<decimal>("request_card_id")),
                },
                CreateDate = row.Field<DateTime>("request_create_date"),
                HasApprovedRequests = Convert.ToBoolean(row.Field<decimal>("has_approved")),
                HasRejectedRequests = Convert.ToBoolean(row.Field<decimal>("has_rejected"))
            };
        }

        public override OracleCommand CreateOracleCommand() {
            var command = new OracleCommand(Query);
            command.Parameters.Add(":card_id", Card == null ? (object)DBNull.Value : Card.Id);
            command.Parameters.Add(":card_id", Card == null ? (object)DBNull.Value : Card.Id);
            command.Parameters.Add(":book_name", string.IsNullOrEmpty(Search) ? (object)DBNull.Value : Search);
            command.Parameters.Add(":book_name", string.IsNullOrEmpty(Search) ? (object)DBNull.Value : Search);
            return command;
        }
    }
}
