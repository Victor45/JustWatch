const input = document.getElementById("searchActorInput");
const ul = document.getElementById("actorsList");
const hiddenInput = document.getElementById("actorsJson");
const rolesContainer = document.getElementById("actorsRolesContainer");

let selectedActors = JSON.parse(hiddenInput.value || "[]").map(a => ({
    id: a.Id ?? a.id,
    name: a.Name ?? a.name,
    role: a.Role ?? a.role,
    castOrder: a.CastOrder ?? a.castOrder
}));

let timeout;

input.addEventListener("keyup", () => {
    clearTimeout(timeout);
    timeout = setTimeout(() => {
        const query = input.value.trim();

        if (query.length < 2) {
            ul.innerHTML = "";
            ul.style.display = "none"
            return;
        }

        fetch(`/Actors/SearchActor?query=${encodeURIComponent(query)}`)
            .then(res => res.json())
            .then(data => {
                ul.innerHTML = "";

                if (!data || data.length === 0) {
                    ul.style.display = "none";
                    return; // ieșim din funcție, nu continuăm
                }

                data.forEach(actor => {
                    const li = document.createElement("li");
                    li.classList.add("actor-found")

                    const span = document.createElement("span");
                    span.textContent = actor.name;

                    const btn = document.createElement("button");
                    btn.type = "button";
                    btn.classList.add("select-actor-btn");

                    // verificăm dacă actorul e deja selectat
                    const isSelected = selectedActors.some(a => a.id === actor.id);
                    btn.textContent = isSelected ? "-" : "+";

                    btn.addEventListener("click", (e) => {
                        e.stopPropagation(); // ← adaugi asta

                        const index = selectedActors.findIndex(a => a.id === actor.id);

                        if (index === -1) {
                            // actorul nu e selectat → adăugăm
                            selectedActors.push({
                                id: actor.id,
                                name: actor.name,
                                role: "",
                                castOrder: selectedActors.length + 1
                            });
                            btn.textContent = "-";
                        } else {
                            // actorul e selectat → eliminăm
                            selectedActors.splice(index, 1);
                            btn.textContent = "+";
                            selectedActors.forEach((a, i) => a.castOrder = i + 1);
                        }

                        // actualizăm hidden input JSON
                        hiddenInput.value = JSON.stringify(selectedActors);
                        renderActorsRoles();
                    });

                    li.appendChild(span);
                    li.appendChild(btn);
                    ul.appendChild(li);
                });

                ul.style.display = "flex";
                ul.style.flexDirection = "column";
            });
    }, 300);
});

input.addEventListener("search", () => {
    ul.innerHTML = "";
    ul.style.display = "none";
});


function renderActorsRoles() {
    rolesContainer.innerHTML = "";

    selectedActors.forEach((actor, index) => {
        const wrapper = document.createElement("div");
        wrapper.classList.add("actors-choice");

        const nameSpan = document.createElement("span");
        nameSpan.classList.add("jw-input", "actor-name");
        nameSpan.textContent = actor.name;

        const roleInput = document.createElement("input");
        roleInput.classList.add("jw-input", "actor-role-input");
        roleInput.placeholder = "Role";
        roleInput.value = actor.role || "";

        const deleteActor = document.createElement("button");
        deleteActor.classList.add("delete-actor-btn");
        deleteActor.type = "button";
        deleteActor.innerHTML = '<i class="bi bi-trash"></i>';

        deleteActor.addEventListener("click", () => {
            selectedActors.splice(index, 1);
            hiddenInput.value = JSON.stringify(selectedActors);
            renderActorsRoles();
        });


        roleInput.addEventListener("input", () => {
            selectedActors[index].role = roleInput.value;
            hiddenInput.value = JSON.stringify(selectedActors);
        });

        wrapper.appendChild(nameSpan);
        wrapper.appendChild(roleInput);
        wrapper.appendChild(deleteActor);
        rolesContainer.appendChild(wrapper);
    })
}