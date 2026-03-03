import { apiCall } from './httpClient.js';
import { loadStats } from "./stats.js";

export async function handleCategorySubmit(e) {
    e.preventDefault();
    const nameInput = document.getElementById("category-name");

    try {
        await apiCall("/categories", 'POST', { name: nameInput.value });
        nameInput.value = "";

        await loadCategories();
        await loadStats();
    } catch (error) {
        console.error("Creating category error: ", error);
    }
}

async function deleteCategory(id) {
    if (!confirm("Are you sure you want to delete the category?")) return;

    try {
        await apiCall(`/categories/${id}`, 'DELETE');

        await loadCategories();
        await loadStats();
    } catch (error) {
        console.error("Deleting category error: ", error);
    }
}

async function turnIntoCategoryEditMode(li, id, currentName) {
    li.innerHTML = `
        <div class="list-item-header">
            <input type="text" class="edit-inline-input" value="${currentName}" />
            <div class="action-buttons">
                <button class="save-btn">Save</button>
                <button class="cancel-btn">Cancel</button>
            </div>
        </div>
    `;

    const input = li.querySelector('.edit-inline-input');
    input.focus();

    li.querySelector('.save-btn').addEventListener('click', () => saveCategory(id, input.value));
    li.querySelector('.cancel-btn').addEventListener('click', () => loadCategories());
}

async function saveCategory(id, newName) {
    if (!newName || newName.trim() === "") {
        document.getElementById("main-error").innerText = "Category name cannot be empty.";
        return;
    }

    try {
        await apiCall("/categories", 'PUT', {
            id: id,
            name: newName.trim()
        });

        await loadCategories();
        await loadStats();
    } catch (error) {
        console.error("Saving category error: ", error);
    }
}

export async function loadCategories() {
    try {
        const categories = await apiCall("/categories");
        const list = document.getElementById("categories-list");
        const select = document.getElementById("category-select");

        list.innerHTML = "";
        select.innerHTML = '<option value="" disabled selected>Select a category...</option>';

        categories.forEach(c => {
            const li = document.createElement("li");
            li.innerHTML = `
                <div class="list-item-header">
                    <span></span>
                    <div class="action-buttons">
                        <button class="edit-btn" data-id="${c.id}">Edit</button>
                        <button class="delete-btn" data-id="${c.id}">Delete</button>
                    </div>
                </div>
            `;
            li.querySelector("span").textContent = c.name;

            li.querySelector(".edit-btn").addEventListener("click", () => turnIntoCategoryEditMode(li, c.id, c.name));
            li.querySelector(".delete-btn").addEventListener("click", () => deleteCategory(c.id));

            list.appendChild(li);

            const option = document.createElement("option");
            option.value = c.id;
            option.textContent = c.name;
            select.appendChild(option);
        });
    } catch (error) {
        console.error("Categories loading error: ", error);
    }
}