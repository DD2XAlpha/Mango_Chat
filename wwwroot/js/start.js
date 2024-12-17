let currentURL = window.location.href.replace('#', '');

$(document).ready(function () {
    $('.genai-option').on('click', function () {
        let selected_model = $(this).data('id');
        let models_available = {};
        let model_option = '';
        $('.selected-border').removeClass('selected-border');
        $(this).addClass('selected-border');
        (async () => {
            try {
                // Example for FetchGet
                let data = await FetchGet(currentURL, { handler: 'Model', modelType: selected_model });
                console.log('GET Response:', data);
                models_available = JSON.parse(data);
                for (var i = 0; i < models_available.length; i++) {
                    model_option += `<option value="${models_available[i].model}">${models_available[i].name}</option>`;
                }

                $('#sl-model').html(model_option);

                $('#config-model').removeAttr('hidden');

            } catch (error) {
                console.error('Error during fetch:', error.message);
            }
        })();
    })

})




