using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TValue>> expression, string labelClass)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName ?? metaData.PropertyName;

            if (metaData.IsRequired)
                labelText += "<span class=\"required-field\">*</span>";

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            label.Attributes.Add("class", labelClass);

            label.InnerHtml = labelText;
            return MvcHtmlString.Create(label.ToString());
        }

        //[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", 
        //    Justification = "This is an appropriate nesting of generic types")]
        //public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, 
        //    Expression<Func<TModel, TValue>> expression, string labelText = "")
        //{
        //    return LabelHelper(html,
        //        ModelMetadata.FromLambdaExpression(expression, html.ViewData),
        //        ExpressionHelper.GetExpressionText(expression), labelText);
        //}

        //private static MvcHtmlString LabelHelper(HtmlHelper html,
        //    ModelMetadata metadata, string htmlFieldName, string labelText)
        //{
        //    if (string.IsNullOrEmpty(labelText))
        //    {
        //        labelText = metadata.DisplayName ?? metadata.PropertyName ?? 
        //            htmlFieldName.Split('.')[htmlFieldName.Split('.').Length - 1];
        //    }

        //    if (string.IsNullOrEmpty(labelText))
        //    {
        //        return MvcHtmlString.Empty;
        //    }

        //    bool isRequired = false;

        //    if (metadata.ContainerType != null)
        //    {
        //        isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)
        //                        .GetCustomAttributes(typeof(RequiredAttribute), false)
        //                        .Length == 1;
        //    }

        //    TagBuilder tag = new TagBuilder("label");
        //    tag.Attributes.Add(
        //        "for",
        //        TagBuilder.CreateSanitizedId(
        //            html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)
        //        )
        //    );

        //    if (isRequired)
        //        tag.Attributes.Add("class", "label-required");

        //    tag.SetInnerText(labelText);

        //    var output = tag.ToString(TagRenderMode.Normal);


        //    if (isRequired)
        //    {
        //        var asteriskTag = new TagBuilder("span");
        //        asteriskTag.Attributes.Add("class", "required");
        //        asteriskTag.SetInnerText("*");
        //        output += asteriskTag.ToString(TagRenderMode.Normal);
        //    }
        //    return MvcHtmlString.Create(output);
        //}
    }
}