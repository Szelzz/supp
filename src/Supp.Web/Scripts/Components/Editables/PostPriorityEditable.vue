<template>
    <div class="d-inline">
        <button v-if="!editMode" v-on:click="edit" class="btn-secret" :disabled="!canEdit">
            <i v-if="value == 'Unset'" title="Nieprzydzielony" class="far fa-fw fa-square"></i>
            <i v-if="value == 'Unimportant'" title="Mało ważny" class="fas fa-fw fa-arrow-down"></i>
            <i v-if="value == 'Normal'" title="Normalny" class="fas fa-fw fa-check"></i>
            <i v-if="value == 'Important'" title="Ważny" class="fas fa-fw fa-exclamation"></i>
        </button>
        <select v-else v-model="value" v-on:change="save" class="form-select form-select-sm d-inline w-auto">
            <option value="Unset" >Nieprzydzielony</option>
            <option value="Unimportant" >Mało ważny</option>
            <option value="Normal" >Normalny</option>
            <option value="Important" >Ważny</option>
        </select>
    </div>
</template>
<script>
    import EditableMixin from './EditableMixin'

    export default {
        mixins: [EditableMixin],
        props: {
            canEdit: Boolean
        }
    }
</script>