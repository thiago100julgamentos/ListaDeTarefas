const idUsuario = localStorage.getItem('IdUsuarioLogado');

function carregarTarefas() {

    document.getElementById("IdUsuario").value =
    localStorage.getItem('IdUsuarioLogado');

    fetch('https://localhost:7199/Tarefa/mostrarTarefa', {

        method: 'GET',
        credentials: 'include'

    })

    .then(response => response.json())

    .then(data => {

        console.log(data);

        var resposta = document.getElementById("respostaConsulta");

        if(data.length === 0){

            resposta.innerHTML =
            "<h4>Você não tem nenhuma tarefa pendente.</h4>";

            return;
        }

        resposta.innerHTML =
        "<h4>Segue Lista de Tarefas</h4>";

        for (i = 0; i < data.length; i++){

            resposta.innerHTML +=

            "<div>" +

            "<li>Descrição: " + data[i].tarefas + "</li>" +

            "<li>Status: " + data[i].status + "</li><br>" +

            "<button onclick='editarTarefa(" + data[i].id + ")'>" +
            "Editar" +
            "</button>" +

            "<button onclick='deletarTarefa(" + data[i].id + ")'>" +
            "Deletar" +
            "</button>" +

            "<hr>" +

            "</div>";
        }

    });
}

function deletarTarefa(id){

    fetch('https://localhost:7199/Tarefa/' + id , {

        method: 'DELETE',
        credentials: 'include'

    })

    .then(response => response.text())

    .then(data => {

        alert(data);

        carregarTarefas();

    });
}

function editarTarefa(id){

    var novaDescricao = prompt("Digite a nova descrição:");

    var novoStatus = prompt("Digite o novo status:");

    fetch('https://localhost:7199/Tarefa/atualizar/' + id , {

        method: 'PUT',

        credentials: 'include',

        headers: {
            'Content-Type': 'application/json'
        },

        body: JSON.stringify({

            descricao: novaDescricao,

            status: novoStatus

        })

    })

    .then(response => response.text())

    .then(data => {

        alert(data);

        carregarTarefas();

    });
}

document.addEventListener('DOMContentLoaded', carregarTarefas);
