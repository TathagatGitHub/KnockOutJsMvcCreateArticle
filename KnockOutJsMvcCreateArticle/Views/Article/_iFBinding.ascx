<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

 <ul data-bind="foreach: products">
        <li>
            Products: <b data-bind="text: name"></b>
            <div data-bind="if: pricing.listprice > 50">
                Cost: <b data-bind="text: pricing.listprice"></b>
            </div>
        </li>

    </ul>