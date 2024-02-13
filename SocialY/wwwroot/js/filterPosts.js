// Function to handle search by title or description
document.getElementById("searchInput").addEventListener("input", function () {
    const searchText = this.value.toLowerCase();
    filterPosts(searchText);
});

// Function to handle search by author
document.getElementById("authorSearchInput").addEventListener("input", function () {
    const authorSearchText = this.value.toLowerCase();
    filterPostsByAuthor(authorSearchText);
});

// Function to filter posts by title or description
function filterPosts(searchText) {
    const rows = document.querySelectorAll(".postRow");
    rows.forEach(function (row) {
        const title = row.querySelector("td:first-child").textContent.toLowerCase();
        const description = row.querySelector("td:nth-child(2)").textContent.toLowerCase();
        if (title.includes(searchText) || description.includes(searchText)) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}

// Function to filter posts by author
function filterPostsByAuthor(authorSearchText) {
    const rows = document.querySelectorAll(".postRow");
    rows.forEach(function (row) {
        const author = row.querySelector("td:nth-child(3)").textContent.toLowerCase();
        if (author.includes(authorSearchText)) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}
