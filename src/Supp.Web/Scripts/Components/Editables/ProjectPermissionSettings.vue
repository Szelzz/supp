<template>
    <div>
        <input v-model="username" type="text" placeholder="Nazwa użytkownika"/>
        <select v-model="role">
            <option v-for="(roleName, role) in roles" :value="role">{{ roleName }}</option>
        </select>
        <button @click="addRole">Dodaj</button>
        <ul>
            <li v-for="userRole in userRoles">
                {{ userRole.username }}
                {{ roles[userRole.role] }}
                <button @click="removeRole(userRole)" v-if="currentUser != userRole.username">X</button>
                <span v-else class="text-muted">Nie można usunąć uprawnień samemu sobie</span>
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
                role: "",
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
                this.userRoles.push({ username: this.username, role: this.role });

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
                    const index = model.userRoles.indexOf(userRole);
                    if (index > -1) {
                        model.userRoles.splice(index, 1);
                    }
                }
            }
        }
    }
</script>