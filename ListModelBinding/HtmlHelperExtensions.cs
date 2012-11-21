using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ListModelBinding
{
    public class ListBinderHelper<T>
    {
        readonly HtmlHelper<T> _htmlHelper;
        readonly string _parameterListName;

        public ListBinderHelper(HtmlHelper<T> htmlHelper, string parameterListName)
        {
            _htmlHelper = htmlHelper;
            _parameterListName = parameterListName;
        }

        public HtmlString CreateListIndex(object indexValue)
        {
            return _htmlHelper.Hidden("list.Index", indexValue);
        }

        public HtmlString TextBoxFor(Expression<Func<T, object>> expression, object indexValue)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, _htmlHelper.ViewData);
            var indexedPropertyName = CreateIndexedPropertyName(modelMetadata.PropertyName, indexValue);
            var value = modelMetadata.SimpleDisplayText;

            return _htmlHelper.TextBox(indexedPropertyName, value);
        }

        public HtmlString DropDownListFor(Expression<Func<T, object>> expression,object indexValue, IEnumerable<SelectListItem> selectListItems)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, _htmlHelper.ViewData);
            var indexedPropertyName = CreateIndexedPropertyName(modelMetadata.PropertyName, indexValue);

            return _htmlHelper.DropDownList(indexedPropertyName, selectListItems);
        }

        private string CreateIndexedPropertyName(string propertyName, object indexValue)
        {
            return _parameterListName + "[" + indexValue + "]." + propertyName;
        }
    }

    public static class HtmlHelperExtensions
    {
        public static ListBinderHelper<T> UseListBinder<T>(this HtmlHelper<T> htmlHelper, string parameterListName)
        {
            return new ListBinderHelper<T>(htmlHelper, parameterListName);
        }

        public static ListBinderHelper<T> UseListBinder<T>(this HtmlHelper<T> htmlHelper)
        {
            return new ListBinderHelper<T>(htmlHelper, "list");
        }
    }

    //public class Reflector
    //{
    //    public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
    //    {
    //        var lambdaEx = expression as LambdaExpression;
    //        if (lambdaEx == null) throw new ArgumentNullException("expression");

    //        MemberExpression memberExpression = null;

    //        if (lambdaEx.Body.NodeType == ExpressionType.Convert)
    //        {
    //            var unaryExpression = lambdaEx.Body as UnaryExpression;
    //            if (unaryExpression == null) throw new ArgumentNullException("expression");

    //            if (unaryExpression.Operand.NodeType == ExpressionType.MemberAccess)
    //            {
    //                memberExpression = unaryExpression.Operand as MemberExpression;
    //            }
    //        }
    //        else if (lambdaEx.Body.NodeType == ExpressionType.MemberAccess)
    //        {
    //            memberExpression = lambdaEx.Body as MemberExpression;
    //        }

    //        if (memberExpression == null) throw new ArgumentNullException("expression");
    //        return memberExpression.Member.Name;
    //    }
    //}
}