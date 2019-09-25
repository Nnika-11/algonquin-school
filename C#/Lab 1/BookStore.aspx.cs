using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //get all books in the catalog.
            List<Book> books = BookCatalogDataAccess.GetAllBooks();
            foreach (Book book in books)
            {
                // Populate dropdown list selections 
                ListItem item = new ListItem(book.Title, book.Id);
                drpBookSelection.Items.Add(item);

            }
        }
        ShoppingCart shoppingcart = null;
        if (Session["shoppingcart"] == null)
        {
            shoppingcart = new ShoppingCart();
            // add cart to the session
            Session["shoppingcart"] = shoppingcart;
        }
        else
        {
            // retrieve cart from the session
            shoppingcart = (ShoppingCart)Session["shoppingcart"];

            foreach (BookOrder order in shoppingcart.BookOrders)
            {
                //Remove the book in the order from the dropdown list
                ListItem item = new ListItem(order.Book.Title, order.Book.Id);
                drpBookSelection.Items.Remove(item);
            }
        }

        if (shoppingcart.NumOfItems == 0)
            lblNumItems.Text = "empty";
        else if (shoppingcart.NumOfItems == 1)
            lblNumItems.Text = "1 item";
        else
            lblNumItems.Text = shoppingcart.NumOfItems.ToString() + " items";
        
    }
    protected void drpBookSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1")
        {
            string bookId = drpBookSelection.SelectedItem.Value;
            Book selectedBook = BookCatalogDataAccess.GetBookById(bookId);

            //Add selected book to the session
            Session["book"] = selectedBook;
            //Display the selected book's description and price 
            lblDescription.Text = selectedBook.Description;
            lblPrice.Text = "$" + selectedBook.Price;
        }
        else
        {
            //Set description and price to blank
            lblDescription.Text = string.Empty;
            lblPrice.Text = string.Empty;
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1" && Session["shoppingcart"] != null)
        {
            // Retrieve selected book from the session
            Book selectedBook = (Book)Session["book"];

            // get user entered quqntity
            int quantity = int.Parse(txtQuantity.Text);
            // Create a book order with selected book and quantity
            BookOrder order = new BookOrder(selectedBook, quantity);
            //Retrieve to cart from the session
            ShoppingCart cart = (ShoppingCart)Session["shoppingcart"];
            // Add book order to the shopping cart
            cart.AddBookOrder(order);
            // Remove the selected item from the dropdown list
            drpBookSelection.Items.Remove(new ListItem(selectedBook.Title, selectedBook.Id));
            // Set the dropdown list's selected value as "-1"
            drpBookSelection.SelectedValue = "-1";
            //Set the description to show title and quantity of the book user added to the shopping cart
            lblDescription.Text = quantity+" copy of " + selectedBook.Title + " is added to the shopping cart";
            //Update the number of items in shopping cart displayed next to the link to ShoppingCartView.aspx
            lblNumItems.Text = cart.NumOfItems.ToString() + " items";
        }
    }
}