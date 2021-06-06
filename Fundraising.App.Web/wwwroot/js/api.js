﻿
function GetProjects() {

    $.ajax({
        url: '/api/API/GetProjects',
        method: 'GET',
        contentType: 'application/json'
    })
        // DONE 
        // -------------------------------
        .done(response => {
            console.log("RESPONSE DATA:");
            console.log(response);


            var table = new Tabulator("#tabulator_id_All_Projects", {
                data: response,
                //autoColumns: true,
                layout: "fitColumns",
                sortOrderReverse: true,
                selectable: true,
                columns: [
                    { title: "title", field: "title", headerFilter: "input" },
                    { title: "description", field: "description" },
                    { title: "amountGathered", field: "amountGathered" },
                    { title: "targetAmount", field: "targetAmount" }
                ]
            })

        })
        // FAIL
        // -------------------------------
        .fail(failure => {
            $('#main').html('ERROR');


        })
        // ALWAYS
        // -------------------------------
        .always(response => {
            console.log(response);

        });
}


function GetProjectsByCreatorId() {

    $.ajax({
        url: '/api/API/GetProjectByCreatorId',
        method: 'GET',
        contentType: 'application/json'
    })
        // DONE 
        // -------------------------------
        .done(response => {
            console.log("RESPONSE DATA GetProjectByCreatorId:");
            console.log(response);


            var table = new Tabulator("#tabulator_id_My_Projects", {
                data: response,
                //autoColumns: true,
                layout: "fitColumns",
                sortOrderReverse: true,
                selectable: true,
                columns: [
                    { title: "title", field: "title", headerFilter: "input" },
                    { title: "description", field: "description" },
                    { title: "amountGathered", field: "amountGathered" },
                    { title: "targetAmount", field: "targetAmount" }
                ]
            })

        })
        // FAIL
        // -------------------------------
        .fail(failure => {
            $('#main').html('ERROR');


        })
        // ALWAYS
        // -------------------------------
        .always(response => {
            console.log(response);

        });
}

$(document).ready(() => {
    GetProjectsByCreatorId();
}
);

$(document).ready(() => {
    GetProjects();
}
);