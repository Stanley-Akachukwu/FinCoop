using System;
using System.Text;
using AP.ChevronCoop.AppCore.ChevronAPIs.Interfaces;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.ChevronAPIs
{
    public class ChevronNetPayService : IChevronNetPayService
    {

        // private readonly Logger<ChevronNetPayService> _logger;
        private readonly CoreAppSettings _CoreAppSettings;
        private readonly CancellationTokenSource cts = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="CoreAppSettings"></param>
        /// <param name="loggerService"></param>
        public ChevronNetPayService(IOptions<CoreAppSettings> CoreAppSettings)
        {
            // _logger = logger;
            _CoreAppSettings = CoreAppSettings.Value;

        }

        /// <summary>
        /// API : To validate if an employee can collect loan return boolean 1- yes | 0 - no
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        public async Task<bool> CanEmployeeCollectLoanAsync(EmployeeCollectLoanRequestDto employeeCollectLoanRequestDto)
        {
            string url = $"{_CoreAppSettings.ChevronBaseUrl}/netpay/canEmployeeAccomodateAdditionalMonthlyDeduction";

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? uri))
            {
                return false;
            }

            employeeCollectLoanRequestDto.Voucher = _CoreAppSettings.ChevronKey;
            string jsonRequest = employeeCollectLoanRequestDto.ToJsonString();

            Dictionary<string, string> headers = new()
                    {
                        { "Accept", "application/json" },
                        {"APIKEY", _CoreAppSettings.ChevronApiKey }
                    };

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using var httpClient = new HttpClient(clientHandler);

            foreach (var item in headers)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, httpContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            string message = $"Email response: {responseContent} Http Status: {response.StatusCode}";
            // _logger.LogInformation(message);

            if (response.IsSuccessStatusCode)
            {
                EmployeeCollectLoanResponseDto? employeeCollectLoanResponseDto = JsonConvert.DeserializeObject<EmployeeCollectLoanResponseDto>(responseContent);

                return !employeeCollectLoanResponseDto?.Error ?? false &&
                    employeeCollectLoanResponseDto?.Value == 1 ? true : false;
            }

            return false;
        }

        /// <summary>
        /// API : To validate if an employee can collect target loan return boolean 1- yes | 0 - no
        /// </summary>
        /// <param name="employeeCollectTargetLoanRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> CanEmployeeCollecTargetLoanAsync(EmployeeCollectTargetLoanRequestDto employeeCollectTargetLoanRequestDto)
        {
            string url = $"{_CoreAppSettings.ChevronBaseUrl}/netpay/canEmployeeCollectTargetLoan";

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? uri))
            {
                return false;
            }

            employeeCollectTargetLoanRequestDto.Key = _CoreAppSettings.ChevronKey;

            string jsonRequest = employeeCollectTargetLoanRequestDto.ToJsonString();

            Dictionary<string, string> headers = new()
                    {
                        { "Accept", "application/json" },
                        {"APIKEY", _CoreAppSettings.ChevronApiKey }
                    };

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using var httpClient = new HttpClient(clientHandler);

            foreach (var item in headers)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, httpContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            string message = $"Email response: {responseContent} Http Status: {response.StatusCode}";
            // _logger.LogInformation(message);

            if (response.IsSuccessStatusCode)
            {
                EmployeeCollectLoanResponseDto? employeeCollectLoanResponseDto = JsonConvert.DeserializeObject<EmployeeCollectLoanResponseDto>(responseContent);

                return !employeeCollectLoanResponseDto?.Error ?? false &&
                   employeeCollectLoanResponseDto?.Value == 1 ? true : false;
            }

            return false;
        }

        /// <summary>
        /// API : To validate if an employee can collect target loan return boolean 1- yes | 0 - no
        /// </summary>
        /// <param name="employeeCollectTargetLoanRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> CanEmployeeCollecOneTimeIncreaseAsync(EmployeeCollectOneTimeIncrease employeeCollectOneTimeIncrease)
        {
            string url = $"{_CoreAppSettings.ChevronBaseUrl}/netpay/canEmployeeGetOneTimeIncrease";

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? uri))
            {
                return false;
            }
            employeeCollectOneTimeIncrease.Key = _CoreAppSettings.ChevronKey;

            string jsonRequest = employeeCollectOneTimeIncrease.ToJsonString();

            Dictionary<string, string> headers = new()
                    {
                        { "Accept", "application/json" },
                        {"APIKEY", _CoreAppSettings.ChevronApiKey }
                    };

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using var httpClient = new HttpClient(clientHandler);

            foreach (var item in headers)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, httpContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            string message = $"Email response: {responseContent} Http Status: {response.StatusCode}";
            // _logger.LogInformation(message);

            if (response.IsSuccessStatusCode)
            {
                EmployeeCollectLoanResponseDto? employeeCollectLoanResponseDto = JsonConvert.DeserializeObject<EmployeeCollectLoanResponseDto>(responseContent);

                return !employeeCollectLoanResponseDto?.Error ?? false &&
                employeeCollectLoanResponseDto?.Value == 1 ? true : false;
            }

            return false;
        }


    }
}

