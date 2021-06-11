


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

            var Category = [
                'ARTS',
                'TECHNOLOGY',
                'SCIENCE',
                'FOOD',
                'MUSIC',
                'SOCIAL'
            ];
            response.forEach(function (row) {
                console.log(row);
                row.category = Category[row.category]
            });
            

            var table = new Tabulator("#tabulator_id_All_Projects", {
                height: "500px",
                data: response,
                //autoColumns: true,
                layout: "fitColumns",
                sortOrderReverse: true,
                selectable: true,
                columns: [
                    { formatter: "rownum", align: "center", width: 40 },
                    {
                        title: "Image", field: "imagePath", formatter: "image", formatterParams: {
                            height: "70px",
                            width: "70px",
                            urlPrefix: "../ProjectImages/"
                        }
                    },
                    { title: "title", field: "title", headerFilter: "input" },
                    {
                        title: "Category", field: "category", headerFilter: true,
                        editor: "select", editorParams: {
                            "ARTS": "ARTS",
                            "TECHNOLOGY": "TECHNOLOGY",
                            "SCIENCE": "SCIENCE",
                            "FOOD": "FOOD",
                            "MUSIC": "MUSIC",
                            "SOCIAL": "SOCIAL"
                        },
                        headerFilterParams: {
                            "ARTS": "ARTS",
                            "TECHNOLOGY": "TECHNOLOGY",
                            "SCIENCE": "SCIENCE",
                            "FOOD": "FOOD",
                            "MUSIC": "MUSIC",
                            "SOCIAL": "SOCIAL"
                        }
                    },
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
                    { formatter: "rownum", align: "center", width: 40 },
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


function GetAllPayments() {

    $.ajax({
        url: '/api/API/GetPaymentsOfProjects',
        method: 'GET',
        contentType: 'application/json'
    })
        // DONE 
        // -------------------------------
        .done(response => {
            console.log("RESPONSE DATA GetAllPayments:");
            console.log(response);


            var table = new Tabulator("#tabulator_id_All_Payments", {
                height: "500px",
                data: response,
                //autoColumns: true,
                layout: "fitColumns",
                sortOrderReverse: true,
                selectable: true,
                columns: [
                    { formatter: "rownum", align: "center", width: 40 },
                    { title: "ProjectName", field: "projectName", headerFilter: "input" },
                    { title: "MemberEmail", field: "memberEmail" },
                    { title: "Date", field: "date" },
                    { title: "Value", field: "value" }
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


$(document).ready(() => {
    GetAllPayments();
}
);
