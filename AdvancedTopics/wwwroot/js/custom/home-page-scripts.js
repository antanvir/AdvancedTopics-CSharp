$(document).ready(function () {
    /* Common */
    populateAllTabsContent();
    $(document).on('click', 'ul li.nav-item a', changeTabContent);

    /* Delegates */
    $(document).on('click', '#btnSubmit', handleDelegateFormSubmission);

    /* Reflection */
    $(document).on('click', '#addItem', showInputArea);
    $(document).on('click', '#submitNewItem', submitNewTableEntry);
});

// #region Common Section

function populateAllTabsContent() {
    getReflectionTabContents();
}

function changeTabContent() {
    const clickedElemTabNo = $(this).data('aria-current');
    $('ul li.nav-item a').not($(this)).removeClass('active');
    $(this).addClass('active');

    $('.tab').each((index, elem) => {
        if ($(elem).data('tabid') === clickedElemTabNo) {
            $(elem).removeClass('hidden');
        } else {
            $(elem).addClass('hidden');
        }
    });
}

/*
* Tab Number:
*    1 -> Delegates & Events
*    2 -> Reflection & Attributes
*/
function getSelectedTabContents(tabNumber) {
    if (tabNumber === "1") {

    } else if (tabNumber === "2") {
        getReflectionTabContents();
    }
}

// #endregion

// #region Delegates

function handleDelegateFormSubmission() {
    submitDelegatesForm().done((response) => {
        onAthleteRegistration(response);
    });
}

function submitDelegatesForm() {
    var def = $.Deferred();

    $(`input[type='checkbox']`).attr("checked", false);
    $(".eligibility-section").addClass("hidden");

    const athlete = {
        Name: $("#Name").val(),
        Height: $("#Height").val(),
        Weight: $("#Weight").val(),
    }

    $.ajax({
        url: 'HomePage/SaveAthlete',
        type: "POST",
        cache: false,
        data: athlete,
        success: function (response) {
            def.resolve(response);
        },
        error: function (err) {
            def.reject(err);
        }
    });
    return def.promise();
}

function onAthleteRegistration(response) {
    $(".eligibility-section").removeClass("hidden");

    if (response.eligibleFor.length > 0) {
        response.eligibleFor.forEach(sport => {
            $(`input[type='checkbox'][name='${sport}']`).attr("checked", true);
        });
    }
}

// #endregion

// #region Reflection

function getReflectionTabContents() {   
    $.ajax({
        url: 'HomePage/GetReflectionTabContent',
        type: "GET",
        cache: false,
        success: function (partialView) {
            $('#reflectionTab').html(partialView);
            $('#reflectionTab').addClass('hidden');     /* Initially remains hidden */
        },
        error: function (err) {
        }
    });
}

function showInputArea() {
    $('#inputSection').removeClass('hidden');
    $('#submitNewItem').removeClass('hidden');
}

function submitNewTableEntry() {
    let newData = {}, key = "", value = "";

    $('#reflectionDataTable #inputSection td input[type="text"]').each((index, elem) => {
        key = $(elem).attr('name');
        value = $(elem).val();
        newData[key] = value;
    });

    const modelData = {
        IsValidData: isValidData,
        DataItems: dataItems,
        ModelType: modelType,
        NewData: newData
    };

    $.ajax({
        url: 'HomePage/AddNewDataInReflectionTab',
        type: "POST",
        cache: false,
        data: modelData,
        success: function (partialView) {
            $('#reflectionTab').html(partialView);
        },
        error: function (err) {
        }
    });
}

// #endregion

