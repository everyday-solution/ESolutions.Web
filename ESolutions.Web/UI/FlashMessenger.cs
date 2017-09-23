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
	[ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
	public class FlashMessenger : WebControl
	{
		//Classes
		#region Notification
		/// <summary>
		/// A single notification.
		/// </summary>
		[Serializable]
		private class Notification
		{
			#region Type
			/// <summary>
			/// Gets or sets the type.
			/// </summary>
			/// <requestValue>The type.</requestValue>
			public MessageTypes Type
			{
				get;
				set;
			}
			#endregion

			#region Message
			/// <summary>
			/// Gets or sets the message.
			/// </summary>
			/// <requestValue>The message.</requestValue>
			public String Message
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region NotificationCollection
		/// <summary>
		/// List of Notifications with aditional method for couting.
		/// </summary>
		[Serializable]
		private class NotificationCollection : List<Notification>
		{
			#region Contains
			/// <summary>
			/// Determines whether the list contains a message of the specified type.
			/// </summary>
			/// <param name="type">The type.</param>
			/// <returns>
			/// 	<c>true</c> if the list contains a message of the specified type; otherwise, <c>false</c>.
			/// </returns>
			public Boolean Contains(MessageTypes type)
			{
				Boolean result = false;

				foreach (Notification current in this)
				{
					if (current.Type == type)
					{
						result = true;
					}
				}

				return result;
			}
			#endregion
		}
		#endregion

		//Properties
		#region Notifications
		/// <summary>
		/// Gets or sets the notifications.
		/// </summary>
		/// <requestValue>The notifications.</requestValue>
		private NotificationCollection Notifications
		{
			get
			{
				NotificationCollection result = null;

				if (this.ViewState["notifications"] != null)
				{
					result = this.ViewState["notifications"] as NotificationCollection;
				}
				else
				{
					this.Notifications = result = new NotificationCollection();
				}

				return result;
			}
			set
			{
				this.ViewState["notifications"] = value;
			}
		}
		#endregion

		#region ErrorCssClass
		/// <summary>
		/// Gets or sets the CSS class for error messages.
		/// </summary>
		/// <requestValue>The warning CSS class.</requestValue>
		[DefaultValue("")]
		[Category("Custom")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public String ErrorCssClass
		{
			get;
			set;
		}
		#endregion

		#region SuccessCssClass
		/// <summary>
		/// Gets or sets the CSS class for success messages.
		/// </summary>
		/// <requestValue>The warning CSS class.</requestValue>
		[DefaultValue("")]
		[Category("Custom")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public String SuccessCssClass
		{
			get;
			set;
		}
		#endregion

		#region ExceptionCssClass
		/// <summary>
		/// Gets or sets the CSS class for exception messages.
		/// </summary>
		/// <requestValue>The warning CSS class.</requestValue>
		[DefaultValue("")]
		[Category("Custom")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public String ExceptionCssClass
		{
			get;
			set;
		}
		#endregion

		#region WarningCssClass
		/// <summary>
		/// Gets or sets the CSS class for warning messages.
		/// </summary>
		/// <requestValue>The warning CSS class.</requestValue>
		[DefaultValue("")]
		[Category("Custom")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public String WarningCssClass
		{
			get;
			set;
		}
		#endregion

		//Methods
		#region RenderContents
		/// <summary>
		/// Renders the contents.
		/// </summary>
		/// <param name="output">The output.</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (this.Notifications.Count > 0)
			{
				output.WriteBeginTag("div");
				output.WriteAttribute("class", this.CssClass);
				output.Write(HtmlTextWriter.TagRightChar);

				this.RenderMessageType(output, MessageTypes.Exception);
				this.RenderMessageType(output, MessageTypes.Error);
				this.RenderMessageType(output, MessageTypes.Warning);
				this.RenderMessageType(output, MessageTypes.Success);

				output.WriteEndTag("div");

				this.Notifications.Clear();
			}
		}
		#endregion

		#region RenderMessageType
		/// <summary>
		/// Renders  the type of the message.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <param name="type">The type.</param>
		private void RenderMessageType(HtmlTextWriter output, MessageTypes type)
		{
			if (this.Notifications.Contains(type))
			{
				//Panel begin tag
				output.WriteBeginTag("div");
				switch (type)
				{
					case MessageTypes.Exception:
					{
						output.WriteAttribute("class", this.ExceptionCssClass);
						break;
					}
					case MessageTypes.Error:
					{
						output.WriteAttribute("class", this.ErrorCssClass);
						break;
					}
					case MessageTypes.Success:
					{
						output.WriteAttribute("class", this.SuccessCssClass);
						break;
					}
					case MessageTypes.Warning:
					{
						output.WriteAttribute("class", this.WarningCssClass);
						break;
					}
				}
				output.Write(HtmlTextWriter.TagRightChar);

				//All message of the specified type
				Int32 messageCount = 0;
				foreach (Notification current in this.Notifications)
				{
					if (current.Type == type)
					{
						output.WriteBeginTag("div");
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(current.Message);
						output.WriteEndTag("div");
						messageCount++;
					}
				}

				//Panel end tag
				output.WriteEndTag("div");
			}
		}
		#endregion

		#region ShowErrorMessage
		/// <summary>
		/// Adds an errormessage to the message queue.
		/// </summary>
		/// <param name="message">The message.</param>
		public void ShowErrorMessage(String message)
		{
			this.Notifications.Add(
				new Notification()
				{
					Message = message,
					Type = MessageTypes.Error
				});
		}
		#endregion

		#region ShowException
		/// <summary>
		/// Add an exception to the message queue. The exception is recursivly parsed.
		/// </summary>
		/// <param name="ex">The ex.</param>
		public void ShowException(Exception ex)
		{
			while (ex != null)
			{
				this.Notifications.Add(
				new Notification()
				{
					Message = ex.Message,
					Type = MessageTypes.Exception
				});
				ex = ex.InnerException;
			}
		}
		#endregion

		#region ShowSuccess
		/// <summary>
		/// Adds a success message to the message queue.
		/// </summary>
		/// <param name="message">The message.</param>
		public void ShowSuccess(String message)
		{
			this.Notifications.Add(
				new Notification()
				{
					Message = message,
					Type = MessageTypes.Success
				});
		}
		#endregion

		#region ShowWarning
		/// <summary>
		/// Adds a warning message to the message queue.
		/// </summary>
		/// <param name="message">The message.</param>
		public void ShowWarning(String message)
		{
			this.Notifications.Add(
				new Notification()
				{
					Message = message,
					Type = MessageTypes.Warning
				});
		}
		#endregion
	}
}
