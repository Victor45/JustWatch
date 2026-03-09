document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("deleteModal");
    const deleteInput = document.getElementById("deleteMovieId");
    const deleteText = document.getElementById("deleteMovieText");
    const cancelBtn = document.getElementById("cancelMovieButton");

    document.querySelectorAll('.delete-movie-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            deleteInput.value = btn.dataset.movieId;
            deleteText.textContent = `Are you sure you want to delete "${btn.dataset.movieTitle}"?`;
            modal.classList.remove('hidden');
        });
    });

    cancelBtn.addEventListener("click", (e) => {
        e.preventDefault();
        modal.classList.add("hidden");
    });

    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            modal.classList.add('hidden');
        }
    });
});
