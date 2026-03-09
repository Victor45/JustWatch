const modal = document.getElementById("newActorModal");
const btn = document.getElementById("newActorBtn");
const span = document.getElementsByClassName("close")[0];
const addBtn = modal.querySelector(".submit-new-actor");

btn.onclick = () => modal.style.display = "flex";
span.onclick = () => modal.style.display = "none";
window.onclick = (event) => { if (event.target == modal) modal.style.display = "none"; };

addBtn.onclick = async () => {
    const container = document.getElementById("newActorForm");
    // creează obiectul actor pentru JSON
    const actor = {
        Name: container.querySelector('[name="Name"]').value || "",
        BirthDate: container.querySelector('[name="BirthDate"]').value || "",
        Description: container.querySelector('[name="Description"]').value || ""
    };

    try {
        const response = await fetch("/Actors/AddNewActor", {
            method: "POST",
            headers: { "Content-Type": "application/json"},
            body: JSON.stringify(actor)
        });

        if (!response.ok) {
            const errorMsg = await response.text();
            document.getElementById("errorMessage").innerText = errorMsg;
            return;
        }

        // success
        document.getElementById("errorMessage").innerText = "";
        modal.style.display = "none";
        container.querySelectorAll("input, textarea").forEach(i => i.value = "");
        alert('Actor added successfully!');

        console.log("Actor added ✅");

    } catch (err) {
        console.error("Failed", err);
        document.getElementById('errorMessage').innerText = 'Something went wrong!';
    }
};
