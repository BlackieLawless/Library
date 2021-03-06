﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Contracts;
using Library.DataContracts.Concrete;
using Library.UI.DevExpressControls.Forms;
using Library.WindowsClient.EditForms;
using Library.WindowsClient.Pages.Abstract;
using Ninject;

namespace Library.WindowsClient.Pages.Concrete
{
    class AuthorPage : Page<IBibliographer, AuthorPage.LoadNecessaryDataWrap, Author>
    {
        public AuthorPage(PageParameters parameters)
            : base(parameters) {
        }

        protected override IEnumerable<Author> LoadDataOperation() {
            return GetProxy().GetAuthors();
        }

        protected override Author CreateDefaultRow() {
            return new Author();
        }

        protected override Author DeleteOperation(Author data) {
            return GetProxy().DeleteAuthor(data);
        }

        protected override TypedEditForm<Author> CreateEditForm() {
            return Ninject.Get<AuthorEditForm>();
        }

        internal class LoadNecessaryDataWrap
        {
        }
    }
}
