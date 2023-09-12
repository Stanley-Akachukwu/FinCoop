using AP.ChevronCoop.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AP.ChevronCoop.Infrastructure
{
    public static class WebValidationUtils
    {

        public static List<ModelValidationError> AllModelErrors(this ModelStateDictionary modelState)
        {
            var result = new List<ModelValidationError>();



            foreach (KeyValuePair<string, ModelStateEntry> modelStatePair in modelState)
            {
                string key = modelStatePair.Key;
                ModelStateEntry modelStateItem = modelStatePair.Value;

                foreach (ModelError error in modelStateItem.Errors)
                {
                    var entry = new ModelValidationError
                    {
                        FieldName = key,
                        Error = error.ErrorMessage,
                        Description = error.Exception?.Message,
                        Value = modelStateItem.AttemptedValue,
                        RawValue = modelStateItem.RawValue

                    };
                    result.Add(entry);
                }
            }


            return result;
        }




    }


}
