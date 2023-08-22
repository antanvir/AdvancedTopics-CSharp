$(document).ready(function () {
    /* Common */
    populateAllTabsContent();
    $(document).on('click', 'ul li.nav-item a', changeTabContent);
    //$(document).on('click', 'ul li.nav-item a', () => alert("Ooops! Something went wrong!"));

    /* Delegates */
    $(document).on('click', '#btnSubmit', handleDelegateFormSubmission);

    /* Reflection */
    $(document).on('click', '#addItem', showInputArea);
    $(document).on('click', '#submitNewItem', submitNewTableEntry);

    /* Design Patterns */
    /*selectDesignPatternsTab();*/
    $(document).on('click', '#btnEnroll', enrollToGymnasium);
    $(document).on('click', '#btnCart, #btnWishList, #btnUndo', updateCustomerCart);

});

// #region Common Section

function populateAllTabsContent() {
    getReflectionTabContents();
    getDesignPatternTabContents();
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
    let def = $.Deferred();

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

// #region Design Patterns

function getDesignPatternTabContents() {
    $.ajax({
        url: 'DesignPattern/GetDesignPatternContents',
        type: "GET",
        cache: false,
        success: function (partialView) {
            $('#designPatternTab').html(partialView);
            $('#designPatternTab').addClass('hidden');     /* Initially remains hidden */
        },
        error: function (err) {
        }
    });
}

function enrollToGymnasium() {
    let subscriberName = $("#subscriberName").val();
    let isLockerChecked = $("input[type='checkbox'][name='locker']").is(":checked");
    let isSwimmingChecked = $("input[type='checkbox'][name='swimming-pool']").is(":checked");
    let isTennisChecked = $("input[type='checkbox'][name='table-tennis']").is(":checked");

    let def = $.Deferred();

    const subscriber = {
        Name: subscriberName,
        IsLockerSubscribed: isLockerChecked,
        IsSwimmingSubscribed: isSwimmingChecked,
        IsTennisSubscribed: isTennisChecked
    }

    $.ajax({
        url: 'DesignPattern/EnrolSubscriber',
        type: "POST",
        cache: false,
        data: subscriber,
        success: function (response) {
            response = response.replaceAll('\r\n', '<br />');
            $("#resultPrint").html(response);
            def.resolve(response);
        },
        error: function (err) {
            def.reject(err);
        }
    });
    return def.promise();
}

function updateCustomerCart() {
    let action = $(this).data('action');
    let def = $.Deferred();

    $.ajax({
        url: 'DesignPattern/UpdateCustomerCart',
        type: "POST",
        cache: false,
        data: {
            ActionName: action
        },
        success: function (partialView) {
            $("#designPatternTab").html(partialView);
            def.resolve(partialView);
        },
        error: function (err) {
            def.reject(err);
        }
    });
    return def.promise();
}
    
// #endregion
