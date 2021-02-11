<template>
    <div class="float-right">
        <button @click="edit">
            <i class="fas fa-pen-square"></i>
        </button>
        <button @click="save">
            <i class="fas fa-save"></i>
        </button>
    </div>
</template>

<script>

    export default {
        props: {
            'updateUrl': String,
            'propertyName': String,
            'modelId': Number,
            'modelValue': String,
            'displayControlId': String,
            'editControlId': String
        },
        data() {
            return {
                value: this.modelValue,
                displayControl: document.getElementById(this.displayControlId),
                editControl: document.getElementById(this.editControlId)
            };
        },
        init: {

        },
        methods: {
            edit() {
                this.displayControl = document.getElementById(this.displayControlId);
                this.editControl = document.getElementById(this.editControlId);
                this.displayControl.style.display = 'none';
                this.editControl.style.display = 'initial';
            },
            save() {
                fetch(this.updateUrl,
                    {
                        method: 'POST',
                        mode: 'cors',
                        cache: 'no-cache',
                        body: this.dataToJson()
                    }
                )
                    .then(response => response.json())
                    .then(data => console.log(data));
            },
            dataToJson() {
                let obj = {
                    modelId: this.modelId,
                    propertyName: this.propertyName,
                    value: this.value
                };
                return JSON.stringify(obj);
            }
        }
    }
</script>

<style lang="scss" scoped>
</style>