<template>
    <span style="display:block">
        <span id="title-display" v-show="!editMode">{{ value }}</span>

        <!-- edit template -->
        <input type="text" class="form-control editable-input"
               v-if="template == 'text'"
               v-show="editMode"
               v-model="value" />
        <textarea class="form-control editable-input"
                  v-if="template == 'textarea'"
                  v-model="value"
                  v-show="editMode"></textarea>

        <span class="float-end">
            <button @click="edit" v-show="!editMode" class="btn btn-sm btn-outline-dark">
                <i class="fas fa-pen-square"></i>
            </button>
            <button @click="save" v-show="editMode" class="btn btn-sm btn-outline-dark">
                <i class="fas fa-save"></i>
            </button>
        </span>
    </span>
</template>

<script>

    export default {
        props: {
            'updateUrl': String,
            'propertyName': String,
            'modelId': Number,
            'modelValue': String,
            'template': {
                type: String,
                default: 'text'
            }
        },
        data() {
            return {
                editMode: false,
                value: this.modelValue,
                displayControl: null,
                editControl: null
            };
        },
        init() {
        },
        methods: {
            getValue() {
                return this.editControl.value;
            },
            edit() {
                this.editMode = true;
            },
            save() {
                fetch(this.updateUrl,
                    {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        cache: 'no-cache',
                        body: this.dataToJson()
                    }
                )
                    .then(r => r.json())
                    .then(this.success)
            },
            success(response) {
                this.value = response;
                this.editMode = false;
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

<style lang="scss">
    .editable-input {
        width: 90%;
        display: inline-block;
    }
</style>