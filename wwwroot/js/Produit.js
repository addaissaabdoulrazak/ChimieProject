$().ready(function () {
    $("#FormAjouter").on("submit", function (event) {
        event.preventDefault()
        ajouter()
    })

    $("#FormEditer").on("submit", function (event) {
        event.preventDefault()
        update()
    })
    $('#example').DataTable();
})


function afficherModal() {

    $("#exampleModal").modal("show")

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
                position: 'top-end',
                icon: 'success',
                title: resultat.message,
                showConfirmButton: false,
                timer: 50000
            })
            location.reload()
        } else {
            Swal.fire({
                position: 'top-end',
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
                position: 'top-end',
                icon: 'success',
                title: resultat.message,
                showConfirmButton: false,
                timer: 50000
            })
            //  location.reload()
        } else {
            Swal.fire({
                position: 'top-end',
                icon: 'warning',
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
                        position: 'top-end',
                        icon: 'success',
                        title: r.message,
                        showConfirmButton: false,
                        timer: 50000
                    })
                    location.reload()

                } else {
                    Swal.fire({
                        position: 'top-end',
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

