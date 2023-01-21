$().ready(function () {
    $("#FormAjouter").on("submit", function (event) {
        event.preventDefault()
        ajouter()
    })

    $("#FormEditer").on("submit", function (event) {
        event.preventDefault()
        update()
    })

    $("#FormRecherche").on("submit", function (event) {
        event.preventDefault()
        rechercheAvance()
    })

})

function rechercheAvance() {
    $.get("/Produit/Rechercher", {
        Nom: $("#Nomrechercher").val(), Formule: $("#Formulerechercher").val(), CAS: $("#CASrechercher").val(), EtatPhysique: $("#EtatPhysiquerechercher").val()
    },
        function (r) {
            if (r.lentgh == 0) {
                alert("aucune donnée trouvé ")
            }
            else {
                $("#tablebody").empty();
               
                r.forEach((item) => {

                    $("#tablebody").append('<tr>' +
                        '<td>'+item.id+'</td>'+
                        '<td>' + item.nom + '</td>' +
                        '<td>' + item.formule + '</td>' +
                        '<td>' + item.cas + '</td>' +
                        '<td>' + item.etatPhysique + '</td>' +
                        '<td>  <button type="submit" class="btn btn-secondary" onclick="rechercher(' + item.id + ')"> <svg xmlns = "http://www.w3.org/2000/svg" width = "16" height = "16" fill = "currentColor" class= "bi bi-pencil-square" viewBox = "0 0 16 16" >'+
                                '<path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />'+
                                '<path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />'+
                            '</svg >'+
                        '</button >' +
                        '<button type="submit" class="btn btn-danger" onclick="Supprimer(' + item.id + ')"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16" >'+
                            '<path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />'+
                            '<path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />'+
                            '</svg >'+
                            '</button >'+
                        '</td > ' +
                        '</tr > ');
                })
            }
            $("#recherchemodal").modal("hide")
           
    })
}
function fermer() {
    $("#recherchemodal").modal("hide")
}
function rechargePage() {
    location.reload()
}
function afficherModal() {

    $("#exampleModal").modal("show")
     

}

function afficherModalRecherche() {

    $("#recherchemodal").modal("show")

}


function ajouter() {

    $.post("/Produit/Add", {
        produit: {
            Id: 0,
            Nom: $("#Nom").val(),
            Formule: $("#Formule").val(),
            CAS: $("#CAS").val(),
            EtatPhysique: $("#EtatPhysique").val()
        }
    }, function (resultat) {
        if (resultat.etat == true) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: resultat.message,
                showConfirmButton: false,
                timer: 50000
            })
            location.reload()
        } else {
            Swal.fire({
                position: 'center',
                icon: 'warning',
                title: resultat.message,
                showConfirmButton: false,
                timer: 1500
            })
        }
    }
    )

}
function rechercher(id) {
    sessionStorage.setItem("idproduit", id)
    $("#Editmodal").modal("show")
    $.get("/Produit/Getbyid", { id: id }, function (r) {
        $("#Nom1").val(r.nom)
        $("#Formule1").val(r.formule)
        $("#CAS1").val(r.cas)
        $("#EtatPhysique1").val(r.etatPhysique)
    })

}
function update() {
    $.post("/Produit/Update", {
        produit: {
            Id: parseInt(sessionStorage.getItem("idproduit")),
            Nom: $("#Nom1").val(),
            Formule: $("#Formule1").val(),
            CAS: $("#CAS1").val(),
            EtatPhysique: $("#EtatPhysique1").val()
        }
    }, function (resultat) {
        if (resultat.etat == true) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: resultat.message,
                showConfirmButton: false,
                timer: 50000
            })
              location.reload()
        } else {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: resultat.message,
                showConfirmButton: false,
                timer: 1500
            })
        }
    }
    )

}

function Supprimer(Id) {



    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get("/Produit/Delete", { id: Id }, function (r) {
                if (r.etat == true) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: r.message,
                        showConfirmButton: false,
                        timer: 50000
                    })
                    location.reload()

                } else {
                    Swal.fire({
                        position: 'center',
                        icon: 'warning',
                        title: r.message,
                        showConfirmButton: false,
                        timer: 1500
                    })
                }

            })

        }
    })
}

