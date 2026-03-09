document.addEventListener("DOMContentLoaded", function () {
    const editBtn = document.getElementById("editMovieBtn");
    const modal = document.getElementById("editModal");
    const editCancel = document.getElementById("editCancelBtn");

    editBtn.addEventListener("click", function () {
        modal.classList.remove("hidden");
    });

    editCancel.addEventListener("click", function () {
        modal.classList.add("hidden");
    });

    modal.addEventListener("click", function (e) {
        if (e.target === modal) {
            modal.classList.add("hidden");
        }
    });
});