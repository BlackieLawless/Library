﻿using Library.Contracts;
using Library.DataAccess.DBInterop.Queries.Concrete;
using Library.DataContracts.Concrete;
using Library.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Monads;

namespace Library.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BibliographerService : AbstractService, IBibliographer
    {
        public IEnumerable<Publisher> GetPublishers() {
            return Ninject.Get<GetPublishersQuery>().Execute();
        }

        public Publisher AddPublisher(Publisher publisher) {
            var query = Ninject.Get<InsertPublisherQuery>();
            query.Publisher = publisher;
            query.Execute();
            return publisher;
        }

        public Publisher UpdatePublisher(Publisher publisher) {
            var query = Ninject.Get<UpdatePublisherQuery>();
            query.Publisher = publisher;
            query.Execute();
            return publisher;
        }

        public Publisher DeletePublisher(Publisher publisher) {
            var query = Ninject.Get<DeletePublisherQuery>();
            query.Publisher = publisher;
            query.Execute();
            return publisher;
        }

        void ClearTree(IEnumerable<Rubric> rubrics) {
            foreach (var rubric in rubrics) {
                rubric.Parent = rubric.Parent == null ? null : new Rubric() {
                    Id = rubric.Parent.Id,
                    Name = rubric.Parent.Name,
                };
                ClearTree(rubric.Childs);
            }
        }

        public IEnumerable<Rubric> GetRubrics() {
            return Ninject.Get<GetRubricsQuery>().Execute();
        }

        public IEnumerable<Rubric> GetRubricsHierarchy() {
            var rubrics = Rubric.FormTree(Ninject.Get<GetRubricsQuery>().Execute()).ToArray();
            ClearTree(rubrics);
            return rubrics;
        }

        public Rubric AddRubric(Rubric rubric) {
            var query = Ninject.Get<InsertRubricQuery>();
            query.Rubric = rubric;
            query.Execute();
            return rubric;
        }

        public Rubric UpdateRubric(Rubric rubric) {
            var query = Ninject.Get<UpdateRubricQuery>();
            query.Rubric = rubric;
            query.Execute();
            return rubric;
        }

        public Rubric DeleteRubric(Rubric rubric) {
            var query = Ninject.Get<DeleteRubricQuery>();
            query.Rubric = rubric;
            query.Execute();
            return rubric;
        }


        public IEnumerable<Book> GetBooks(Rubric rubric = null, Publisher publisher = null, string search = "") {
            var books = Ninject.Get<GetBooksQuery>().Execute();
            books = rubric.Return(r => books.Where(b => b.Rubric.Id == r.Id), books);
            books = publisher.Return(p => books.Where(b => b.Publisher.Id == p.Id), books);
            books = string.IsNullOrEmpty(search) ? books : from b in books
                                                           let s = search.ToLower()
                                                           where b.Name.ToLower().Contains(s) || b.Annotation.ToLower().Contains(s)
                                                           select b;
            return books.ToArray();
        }

        public Book AddBook(Book book) {
            throw new NotImplementedException();
        }

        public Book UpdateBook(Book book) {
            throw new NotImplementedException();
        }

        public Book DeleteBook(Book book) {
            throw new NotImplementedException();
        }
    }
}
