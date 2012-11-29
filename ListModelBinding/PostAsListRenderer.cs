using System;
using System.Linq.Expressions;
using MvcContrib.UI.Grid;

namespace ListModelBinding
{
    public class Reflector
    {
        public static string GetPropertyName(Expression<Action> expression)
        {
            return string.Empty;
        }

        public static string GetPropertyName(Expression<Func<object, object>> expression)
        {
            return GetPropertyName<object, object>(expression);
        }

        public static string GetPropertyName<T, T1>(Expression<Func<T, T1>> expression)
        {
            var lambdaEx = expression as LambdaExpression;
            if (lambdaEx == null) throw new ArgumentNullException("expression");

            var text = ExpressionHelper.GetExpressionText(lambdaEx);
            
            MemberExpression memberExpression = null;

            if (lambdaEx.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = lambdaEx.Body as UnaryExpression;
                if (unaryExpression == null) throw new ArgumentNullException("expression");

                if (unaryExpression.Operand.NodeType == ExpressionType.MemberAccess)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }
            else if (lambdaEx.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = lambdaEx.Body as MemberExpression;
            }

            if (memberExpression == null) throw new ArgumentNullException("expression");
            return memberExpression.Member.Name;
        }


        //public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        //{
        //    var lambdaEx = expression as LambdaExpression;
        //    if (lambdaEx == null) throw new ArgumentNullException("expression");

        //    MemberExpression memberExpression = null;

        //    if (lambdaEx.Body.NodeType == ExpressionType.Convert)
        //    {
        //        var unaryExpression = lambdaEx.Body as UnaryExpression;
        //        if (unaryExpression == null) throw new ArgumentNullException("expression");

        //        if (unaryExpression.Operand.NodeType == ExpressionType.MemberAccess)
        //        {
        //            memberExpression = unaryExpression.Operand as MemberExpression;
        //        }
        //    }
        //    else if (lambdaEx.Body.NodeType == ExpressionType.MemberAccess)
        //    {
        //        memberExpression = lambdaEx.Body as MemberExpression;
        //    }

        //    if (memberExpression == null) throw new ArgumentNullException("expression");
        //    return memberExpression.Member.Name;
        //}
    }

    public class PostAsListRenderer<T> : HtmlTableGridRenderer<T> where T : class
    {
        private readonly string _collectionName;
        private string _previousHtmlFieldPrefix;
        Guid _currentIndex;
        bool _firstCellRendered;

        public PostAsListRenderer(string collectionName)
        {
            _collectionName = collectionName;
        }

        protected override void RenderStartCell(GridColumn<T> column, GridRowViewData<T> rowData)
        {
            string attrs = BuildHtmlAttributes(column.Attributes(rowData));
            if (attrs.Length > 0)
                attrs = " " + attrs;

            var hidden = string.Empty;
            if (!_firstCellRendered)
            {
                hidden = string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", _collectionName, _currentIndex);    
            }

            _firstCellRendered = true;
            
            RenderText(string.Format("<td{0}>{1}", attrs, hidden));
        }

        protected override void RenderRowStart(GridRowViewData<T> rowData)
        {
            _firstCellRendered = false;
            _currentIndex = Guid.NewGuid();
            
            Context.ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("{0}[{1}]", _collectionName, _currentIndex);
            
            base.RenderRowStart(rowData);
        }

        protected override void RenderGridStart()
        {
            _previousHtmlFieldPrefix = Context.ViewData.TemplateInfo.HtmlFieldPrefix;
            
           base.RenderGridStart();
        }

        protected override void RenderGridEnd(bool isEmpty)
        {
            Context.ViewData.TemplateInfo.HtmlFieldPrefix = _previousHtmlFieldPrefix;
            base.RenderGridEnd(isEmpty);
        }
    }
}