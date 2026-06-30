// GET all
fetch("http://localhost:5219/todos")
    .then(response => response.json())
    .then(data => console.log("All todos:", data))

// POST - add new task
fetch("http://localhost:5219/todos", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ name: "Learn JavaScript", isComplete: false })
})
    .then(response => response.json())
    .then(data => console.log("Added:", data))

// PUT - mark task 1 complete
fetch("http://localhost:5219/todos/1", {
    method: "PUT"
})
    .then(response => response.json())
    .then(data => console.log("Updated:", data))

// DELETE - delete task 3
fetch("http://localhost:5219/todos/3", {
    method: "DELETE"
})
    .then(response => response.json())
    .then(data => console.log("Deleted:", data))