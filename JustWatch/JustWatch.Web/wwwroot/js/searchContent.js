<script>
        (() => {
          const box = document.getElementById("navSearchBox");
    const list = document.getElementById("navSuggestions");
    if (!box || !list) return;

    let timer = null;

    function hide() {
        list.innerHTML = "";
    list.classList.remove("open");
          }

          box.addEventListener("input", () => {
        clearTimeout(timer);
            timer = setTimeout(async () => {
              const q = box.value.trim();
    if (q.length < 2) {hide(); return; }

    const res = await fetch(`/Home/Search?q=${encodeURIComponent(q)}`);
    const data = await res.json();

    if (!data.length) {hide(); return; }

              list.innerHTML = data.map(x => {
                const url = (x.type === "Movie")
    ? `/Movies/Details/${x.id}`
    : `/TVShows/Details/${x.id}`;

    return `
    <a class="s-item" href="${url}">
        <div class="s-poster-card">
            <img src="${x.posterURL}"></img>
        </div>
        <div class="s-details">
            <span class="s-title">${x.title}</span>
            <span class="s-info">${x.info} ${x.type === "TVShow" ? " Seasons" : ""}</span>
            <span class="s-kind">${x.type}</span>
        </div>
    </a>`;
              }).join("");

    list.classList.add("open");
            }, 400);
          });

          document.addEventListener("click", (e) => {
            if (!list.contains(e.target) && e.target !== box) hide();
          });

          box.addEventListener("keydown", (e) => {
            if (e.key === "Escape") hide();
          });

          box.addEventListener("keydown", (e) => {
            if (e.key === "Enter") {
        document.getElementById("isFromButton").value = "true";
          }
        });
        })();
</script>