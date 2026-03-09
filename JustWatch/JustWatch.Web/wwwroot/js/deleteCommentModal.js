document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("deleteCommentModal");
    const deleteInput = document.getElementById("deleteCommentId");
    const deleteText = document.getElementById("deleteCommentText");
    const cancelBtn = document.getElementById("cancelCommentButton");

    document.querySelectorAll('.delete-comm-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            deleteInput.value = btn.dataset.commId;
            deleteText.textContent = `Are you sure you want to delete ${btn.dataset.commUser}'s comment?`;
            modal.classList.remove('hidden');
        });
    });

    cancelBtn.addEventListener("click", (e) => {
        e.preventDefault();
        modal.classList.add("hidden");
    })

    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            modal.classList.add('hidden');
        }
    });
});
