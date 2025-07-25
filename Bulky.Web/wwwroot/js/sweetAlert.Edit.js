function editCategory(id, currentName) {
    Swal.fire({
        title: 'Edit Category',
        input: 'text',
        inputLabel: 'Category Name',
        inputValue: currentName,
        showCancelButton: true,
        confirmButtonText: 'Update',
        inputValidator: (value) => {
            if (!value) {
                return 'You need to write a category name!';
            }
        }
    }).then(result => {
        if (result.isConfirmed) {
            const name = result.value;
            fetch(`/Category/Edit`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id, name })
            }).then(res => location.reload())
        }
    });
}