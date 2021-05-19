# BookEditor
Application used to control Visma's library

# Available commands
**Use these commands to control application.**

#
**quit** - quits application.

#
**add** (book_name) (author) (category) (language) (publication_date) (isbn) - add new book to library

**Example:** add Vagis Jonas_Biliunas drama lt 1905-06-12 9789986094814

#
**readall** (book_property) - lists all books descending order.

**Example:** readall isbn

**available properties:** name, author, category, language, isbn, taken.

#
**take** (book_name) (user) (period_month) - gives book to user.

**Example:** take Vagis anonymous 1

#
**return** (book_name) (user) - removes book from user.

**Example:** return Vagis anonymous

#
**delete** (book_name) or (author) - removes book from library.

**Example: delete Vagis
