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
	public class ImageLinkButton : System.Web.UI.WebControls.LinkButton
	{
		//Classes
		#region ImagePositions
		/// <summary>
		/// The possible position of images in relation to the text.
		/// </summary>
		public enum ImagePositions
		{
			/// <summary>
			/// The text is displayed left next to the text.
			/// </summary>
			FirstTextThenImage,
			/// <summary>
			/// The image is displayed right from the text.
			/// </summary>
			FirstImageThenText
		}
		#endregion

		//Fields
		#region image
		private Image image = new Image();
		#endregion

		#region text
		private Literal text = new Literal();
		#endregion

		//Properties
		#region ImagePosition
		/// <summary>
		/// Gets or sets the position of the image in relation to the text.
		/// </summary>
		/// <value>The image position.</value>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(false)]
		public ImagePositions ImagePosition
		{
			get
			{
				ImagePositions result = ImagePositions.FirstImageThenText;

				if (this.ViewState["ImagePosition"] != null)
				{
					result = (ImagePositions)this.ViewState["ImagePosition"];
				}

				return result;
			}

			set
			{
				ViewState["ImagePosition"] = value;
			}
		}
		#endregion

		#region ImageUrl
		/// <summary>
		/// Gets or sets the image URL.
		/// </summary>
		/// <value>The image URL.</value>
		[Category("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		[UrlProperty]
		[Bindable(true)]
		[DefaultValue("")]
		public String ImageUrl
		{
			get
			{
				return this.image.ImageUrl;
			}
			set
			{
				this.image.ImageUrl = value;
			}
		}
		#endregion

		#region Text
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The image URL.</value>
		[Bindable(true)]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		public new String Text
		{
			get
			{
				return this.text.Text;
			}
			set
			{
				this.text.Text = value;
			}
		}
		#endregion

		//Constructors
		#region ImageLinkButton
		public ImageLinkButton()
		{
			this.image.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
			
			switch (this.ImagePosition)
			{
				case ImagePositions.FirstImageThenText:
				{
					this.image.Style.Add(HtmlTextWriterStyle.MarginRight, "5px");
					this.Controls.Add(this.image);
					this.Controls.Add(this.text);
					break;
				}
				case ImagePositions.FirstTextThenImage:
				{
					this.image.Style.Add(HtmlTextWriterStyle.MarginLeft, "5px");
					this.Controls.Add(this.text);
					this.Controls.Add(this.image);
					break;
				}
			}
		}
		#endregion
	}
}
