﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESolutions.Web.UI {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ActiveQueryExceptionStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ActiveQueryExceptionStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ESolutions.Web.UI.ActiveQueryExceptionStrings", typeof(ActiveQueryExceptionStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The query contains the parameter {0} with an unallowed value of {1}..
        /// </summary>
        internal static string ArgumentValueIsNotSupported {
            get {
                return ResourceManager.GetString("ArgumentValueIsNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type could not be changed..
        /// </summary>
        internal static string CantChangeType {
            get {
                return ResourceManager.GetString("CantChangeType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class {0} could not be deserialized. See inner exceptions for further information..
        /// </summary>
        internal static string CantDeserialize {
            get {
                return ResourceManager.GetString("CantDeserialize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The paramater field {0} could not be deserialized..
        /// </summary>
        internal static string CantDeserializeProperty {
            get {
                return ResourceManager.GetString("CantDeserializeProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class {0} has no nested class with the PageQuery-Attribute.
        /// </summary>
        internal static string NoClassWithPageQueryAttribute {
            get {
                return ResourceManager.GetString("NoClassWithPageQueryAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class {0} has no PageUrl-Attribute..
        /// </summary>
        internal static string NoPageUrlAttribute {
            get {
                return ResourceManager.GetString("NoPageUrlAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value of the parameter {0} is null but the parameter is not marked as optional..
        /// </summary>
        internal static string ParameterIsNullButNonOptional {
            get {
                return ResourceManager.GetString("ParameterIsNullButNonOptional", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class {0} has more than on nested classes decorated with the PageQuery-Attribute..
        /// </summary>
        internal static string ToManyPageQueryClasses {
            get {
                return ResourceManager.GetString("ToManyPageQueryClasses", resourceCulture);
            }
        }
    }
}
