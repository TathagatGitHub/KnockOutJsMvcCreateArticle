<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

Products: <input data-bind="value: product" /> Cost: <input data-bind="value: cost" />
                        Display: <b data-bind="text: product"></b>
                        Cost: <b data-bind="text: cost"></b>