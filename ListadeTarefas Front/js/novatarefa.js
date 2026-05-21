const myForm1 = document.getElementById('adicionarTarefa');
myForm1.addEventListener('submit', function (event) {
   
    event.preventDefault();

    const idUsuario = localStorage.getItem('IdUsuarioLogado');

    fetch('https://localhost:7199/Tarefa/criarTarefa', {
        method: 'POST', 
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            descricao: document.getElementById("descricao").value,
            status: document.getElementById("status").value,
           
            usuarioId: idUsuario
        }),
    }).then(response => response.json())
        .then(data => {
            document.getElementById("respostaTarefa").innerHTML ="<h4>Nova Tarefa adicionada com sucesso!</h4>";        
        })
        .catch(error => {
            console.error('Erro:', error);
            document.getElementById("respostaTarefa").innerHTML = "<h4 style='color: red;'>Erro ao adicionar tarefa.</h4>";
        });
});
