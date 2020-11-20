
function exibirImagem(fileImg, previewImg) {
    var oFReader = new FileReader();
    oFReader.readAsDataURL(document.getElementById(fileImg).files[0]);
    oFReader.onload = function (oFREvent) {
        document.getElementById(previewImg).src = oFREvent.target.result;
    };
}

function confirmaExclusao(id) {
    if (confirm('Deseja mesmo apagar este registro?'))
        location.href = '/produto/Excluir?id=' + id;
}

function buscaCEP() {
    var cep = document.getElementById("cep").value;
    cep = cep.replace('-', '');
    if (cep.length > 0) {
        var linkAPI = 'https://viacep.com.br/ws/' + cep + '/json/';

        $.ajax({
            type: 'GET',
            url: linkAPI,
            datatype: "json",
            cache: false,
            success: function (dados) {
                if (dados.erro != undefined)  // quando o CEP não existe...
                {
                    alert('CEP não localizado...');
                    document.getElementById("logradouro").value = '';
                    document.getElementById("bairro").value = '';
                    document.getElementById("localidade").value = '';
                    document.getElementById("uf").value = '';
                }
                else // quando o CEP existe
                {
                    document.getElementById("logradouro").value = dados.logradouro;
                    document.getElementById("bairro").value = dados.bairro;
                    document.getElementById("localidade").value = dados.localidade;
                    document.getElementById("uf").value = dados.uf;
                }
            }
        });
    }
}


