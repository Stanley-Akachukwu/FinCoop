using Microsoft.JSInterop;
using System;
using System.IO;
using System.Threading.Tasks;
namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
	public class FileDownloader
	{
		private readonly IJSRuntime _jsRuntime;

		public FileDownloader(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task DownloadFileFromBase64Async(string base64Content, string fileType)
		{
			byte[] fileBytes;
			try
			{
				// Convert the base64 string to bytes
				fileBytes = Convert.FromBase64String(base64Content);
			}
			catch (FormatException)
			{
				throw new ArgumentException("Invalid base64 format.");
			}

			string fileName = $"downloaded_file.{fileType}";

			try
			{
				// Save the file to the client-side using JavaScript interop
				await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileBytes);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while downloading the file.", ex);
			}
		}
		public async Task DownloadFileFromBase64Async(byte[] fileBytes, string fileType)
		{
			string fileName = $"downloaded_file.{fileType}";

			try
			{
				// Save the file to the client-side using JavaScript interop
				await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileBytes);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while downloading the file.", ex);
			}
		}
	}
}
