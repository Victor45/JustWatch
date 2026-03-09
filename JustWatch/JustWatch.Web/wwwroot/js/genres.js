const grid = document.getElementById("genresGrid");
const hiddenGenres = document.getElementById("genresJson");

let selectedGenres = [];

fetch(`/Genres/GetAllGenres`)
    .then(res => res.json())
    .then(data => {
        grid.innerHTML = "";

        data.forEach(genre => {
            const btn = document.createElement("button");
            btn.type = "button";
            btn.classList.add("jw-input", "genre-btn");
            btn.textContent = genre.name;

            const isSelected = selectedGenres.some(g => g.id === genre.id);

            if (isSelected) {
                btn.classList.add("selected");
            }

            btn.addEventListener("click", (e) => {

                const index = selectedGenres.findIndex(g => g.id === genre.id);

                if (index === -1) {
                    selectedGenres.push({
                        id: genre.id,
                        name: genre.name
                    });
                    btn.classList.add("selected");
                } else {
                    selectedGenres.splice(index, 1);
                    btn.classList.remove("selected");
                }

                hiddenGenres.value = JSON.stringify(selectedGenres);
            })

            grid.appendChild(btn);
        });
    });