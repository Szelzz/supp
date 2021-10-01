<template>
    <div>
        <div class="input-group mb-3">

            <input class="form-control" v-model="username" type="text" placeholder="Nazwa użytkownika" />
            <select class="form-select" v-model="role">
                <option v-for="(roleName, role) in roles" :value="role">{{ roleName }}</option>
            </select>
            <button class="btn btn-outline-secondary" @click="addRole">Dodaj</button>
        </div>


        <ul class="list-group">
            <li class="list-group-item" v-for="userRole in userRolesList">
                <div class="row">

                    <div class="col-3">
                        {{ userRole.username }}

                    </div>
                    <div class="col">
                        <div class="vr"></div>
                        {{ roles[userRole.role] }}
                    </div>
                    <div class="col-auto">
                        <span v-if="currentUser == userRole.username" class="text-muted">Nie można usunąć uprawnień samemu sobie</span>
                        <button class="btn-close" @click="removeRole(userRole)" v-if="currentUser != userRole.username"></button>
                    </div>
                </div>
            </li>
        </ul>

    </div>
</template>
<script>
    import Ajax from '../../Ajax';
    export default {
        props: {
            projectId: Number,
            userRoles: Array,
            roles: Object,
            addUrl: String,
            removeUrl: String,
            currentUser: String,
        },
        data() {
            return {
                username: "",
                role: "ProjectVisitor",
                userRolesList: this.userRoles
            }
        },
        created() {
        },
        methods: {
            addRole() {
                Ajax.apiRequest(this.addUrl,
                    { projectId: this.projectId, username: this.username, role: this.role },
                    this.addRoleSuccess);
            },
            addRoleSuccess(response) {
                this.userRolesList.push({ username: this.username, role: this.role });

                this.username = null;
                this.role = null;
            },
            removeRole(userRole) {
                Ajax.apiRequest(
                    this.removeUrl,
                    { projectId: this.projectId, username: userRole.username, role: userRole.role },
                    this.removeRoleSuccessGenerator(userRole));
            },
            removeRoleSuccessGenerator(userRole) {
                const model = this;
                return function (response) {
                    const index = model.userRolesList.indexOf(userRole);
                    if (index > -1) {
                        model.userRolesList.splice(index, 1);
                    }
                }
            }
        }
    }
</script>