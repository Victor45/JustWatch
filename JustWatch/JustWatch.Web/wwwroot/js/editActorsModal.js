document.addEventListener("DOMContentLoaded", function () {
    const editBtn = document.getElementById("editActorsBtn");
    const modal = document.getElementById("editActorsModal");
    const editCancel = document.getElementById("editActorsCancelBtn");

    editBtn.addEventListener("click", function () {
        modal.classList.remove("hidden");
        renderActorsRoles();
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