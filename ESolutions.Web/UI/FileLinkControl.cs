using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESolutions.Web.UI
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:FileLinkControl runat=server></{0}:FileLinkControl>")]
	public class FileLinkControl : WebControl
	{
		//Properties
		#region File
		/// <summary>
		/// Gets or sets the path to the file that is linked.
		/// </summary>
		/// <value>
		/// The path and name of the file.
		/// </value>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string File
		{
			get
			{
				var value = (String)this.ViewState[nameof(File)];
				return value ?? String.Empty;
			}
			set
			{
				this.ViewState[nameof(File)] = value;
			}
		}
		#endregion

		#region File
		/// <summary>
		/// Gets or sets a value indicating whether to automatic invalidate the linked file.
		/// </summary>
		/// <value>
		///   <c>true</c> if linked file is automatically invalidate; otherwise, <c>false</c>.
		/// </value>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public Boolean AutoInvalidate
		{
			get
			{
				Boolean? value = (Boolean?)this.ViewState[nameof(AutoInvalidate)];
				return value ?? false;
			}
			set
			{
				this.ViewState[nameof(AutoInvalidate)] = value;
			}
		}
		#endregion

		//Methods
		#region RenderContents
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (this.File.EndsWith(".css"))
			{
				output.WriteBeginTag("link");
				output.WriteAttribute("href", this.Page.ResolveUrl(this.File) + this.RenderInvalidationToken());
				output.WriteAttribute("rel", "stylesheet");
				output.WriteAttribute("type", "text/css");
				output.Write(HtmlTextWriter.SelfClosingTagEnd);
			}
			else if (this.File.EndsWith(".ico"))
			{
				output.WriteBeginTag("link");
				output.WriteAttribute("href", this.Page.ResolveUrl(this.File) + this.RenderInvalidationToken());
				output.WriteAttribute("rel", "shortcut icon");
				output.WriteAttribute("type", "image/x-icon");
				output.Write(HtmlTextWriter.SelfClosingTagEnd);
			}
			else
			{
				output.WriteBeginTag("script");
				output.WriteAttribute("src", this.Page.ResolveUrl(this.File) + this.RenderInvalidationToken());
				output.WriteAttribute("type", "text/javascript");
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteEndTag("script");
			}
		}
		#endregion

		#region RenderInvalidationToken
		private String RenderInvalidationToken()
		{
			return this.AutoInvalidate ?
				$"?version={DateTime.UtcNow.Ticks}" :
				String.Empty;
		}
		#endregion

		#region RenderControl
		public override void RenderControl(HtmlTextWriter writer)
		{
			this.RenderContents(writer);
		}
		#endregion
	}
}
