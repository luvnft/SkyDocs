﻿using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyDocs.Blazor.Pages.Modals
{
    public partial class ShareModal
    {
        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public SkyDocsService SkyDocsService { get; set; } = default!;

        [Inject]
        public MetaMaskService MetaMaskService { get; set; } = default!;

        [Inject]
        public ShareService ShareService { get; set; } = default!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        public bool ShareReadOnly { get; set; } = true;

        public string ShareText  => ShareReadOnly ? "Anyone with the link can view the document" : "Anyone with the link can edit the document";

        public ShareFormModel ShareFormModel { get; set; } = new ShareFormModel();

        public string CopyText { get; set; } = "Copy";

        public string? Error { get; set; }

        private void SetShareUrl(bool readOnly)
        {
            var sum = SkyDocsService.CurrentSum;
            if (sum != null)
            {
                ShareService.CurrentShareUrl = ShareService.GetShareUrl(sum, readOnly);
            }
        }

        void OnRadioButtonChange(bool value)
        {
            SetShareUrl(value);
            CopyText = "Copy";
            StateHasChanged();
        }

        private void OnMetaMaskShare()
        {

        }

        private async Task CopyTextToClipboard()
        {
            await JSRuntime.InvokeVoidAsync("clipboardCopy.copyText", ShareService.CurrentShareUrl);
            CopyText = "Copied!";
        }
    }

    public class ShareFormModel
    {
        public string EthAddress { get; set; }
    }
}
