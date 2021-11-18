<template>
    <span style="display: block; white-space: normal" class="editable-area d-flex" ref="editableArea">
        <span class="flex-fill">
            <span id="title-display" v-show="!editMode">{{ value }}</span>

            <!-- edit template -->
            <input type="text" class="form-control"
                   v-if="template == 'text'"
                   v-show="editMode"
                   v-model="value" />
            <textarea class="form-control"
                      v-if="template == 'textarea'"
                      v-model="value"
                      v-show="editMode"></textarea>
        </span>
        <span v-if="canEdit">
            <button @click="edit" v-show="!editMode" class="btn btn-sm btn-outline-dark" @mouseover="buttonMouseOver" @mouseout="buttonMouseOut">
                <i class="fas fa-pen-square"></i>
            </button>
            <button @click="save" v-show="editMode" class="btn btn-sm btn-outline-dark" @mouseover="buttonMouseOver" @mouseout="buttonMouseOut">
                <i class="fas fa-save"></i>
            </button>
        </span>
    </span>
</template>

<script>
    import EditableMixin from './EditableMixin'

    export default {
        mixins: [EditableMixin]
    }
</script>

<style lang="scss">
    .editable-area-hover {
        background-color: rgba(0, 0, 0, 0.10);
    }
</style>