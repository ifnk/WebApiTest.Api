using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApiTest.Api.Helpers
{
    //自定义model 绑定器
    public class ArrayModelBinder : IModelBinder
    {
        public Task
            BindModelAsync(ModelBindingContext bindingContext) //将 "1,2,3,4" 这样的字符串转换 为  ["1","2","3","4"] 这种类型的 数组 
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType) //判断 传进来 的类型 是不是数组
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString(); // value ="1,1,3"
            if (string.IsNullOrWhiteSpace(value)) // "  " 这种都可以判断  判断 字符串 
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }
            var elementType =
                bindingContext.ModelType.GetTypeInfo()
                    .GenericTypeArguments[0]; //{Name = "Guid" FullName = "System.Guid"} 获取类型 是 guid 
            var converter =
                TypeDescriptor.GetConverter(elementType); //把字符串类型 转换成 guid 类型 {System.ComponentModel.GuidConverter}
             var values = value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries) //guid 对象 组成的数组  [object1,object2,object3]
                 .Where(IsGuidByParse)
               .Select(x => converter.ConvertFromString(IsGuidByParse(x.Trim()) ? x.Trim() : "")).ToArray();
            //将object 类型 转换成数组类型
            var typedValues = Array.CreateInstance(elementType, values.Length); //elementType = {Name = "Guid" FullName = "System.Guid"}， typedValues = {System.Guid[2]}
            values.CopyTo(typedValues, 0); //typedValues = {System.Guid[2]}  [0] = {f3f53ae8-be12-4c23-8aee-9489187f2551}, [1] = {9a8bf408-0e59-42ab-803a-c5689237343a}
            bindingContext.Model = typedValues; //bindingContext.Model = {System.Guid[2]}   [0] = {f3f53ae8-be12-4c23-8aee-9489187f2551}, [1] = {9a8bf408-0e59-42ab-803a-c5689237343a}
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model); //bindingContext.Result = {Success 'System.Guid[]'} Model = {System.Guid[2]}
            return Task.CompletedTask;
        }

        public static bool IsGuidByParse(string strSrc)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(strSrc, out g);
        }
    }
}