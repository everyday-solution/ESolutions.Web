# ESolutions.Web
Tools for ASP.WebForms including an URL/Object-Mapper (like O/R for Urls), URL-Generation and error handling for masterpages. 
Main feature of this library is a System to navigate type safe from and to WebForm pages.

## URL/Object-Mapping (U/R-Mapping)
Each WebPage is decorated with a PageUrlAttribute that defines its relative position in the folder structure.
If a WebPage need url-parameters they can be defined in a separate class. The WebPage then needs to derive the ESolutions.Web.UI.WebPage 
base class either with or without the query type. 

Additionally the WebPage class contains a RequestAddOn and a ResponseAddOn that can be used to navigate to the decorated classes. 
Navigation and queries are created type safe.

A short sample

```cs
[ESolutions.Web.UI.PageUrl("~/TestWebPage.aspx")]
public class TestWebPage : ESolutions.Web.UI.Page<TestWebPage.Query>
{
	[PageQuery]
	public class Query : ActiveQueryBase<Query>
	{
		[UrlParameter]
		public Int32 Id
		{
			get;
			set;
		}

		[UrlParameter]
		public String Text
		{
			get;
			set;
		}
	}
		
	protected void Page_PreRender(Object sender, EventArgs e)
	{
	    //Demo for creating typed links
	    this.AnyLink = PageUrlAttribute.Get<TestWebPage>(new TestWebPage.Query()
	    {
		Id = 55,
		Text = "My text"
	    });
            
	    //Demo for accessing url parameters
	    var id = this.RequestAddOn.Query.Id;
	    var text = this.RequestAddOn.Query.Text;
	}
		
	protected void AnyButton_Click(Object sender, EventArgs e)
	{
		//Demo for redirection
		this.ResponseAddOn.Redirect<YetAnotherPage>();
	}
}
```
