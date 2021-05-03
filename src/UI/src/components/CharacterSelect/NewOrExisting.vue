<template>
    <v-scale-transition origin="center">
        <v-container v-if="Show" class="main">
            <v-container class="panel">
                <v-card class="card" dark>
                    <h1>Character Select</h1>
                    <v-divider class="my-4" />
                    <v-btn block @click="NewCharacter()">New Character</v-btn>
                    <v-btn block>Select Character</v-btn>
                </v-card>
            </v-container>
        </v-container>
    </v-scale-transition>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class NewOrExisting extends Vue {
        show = true;

        NewCharacter() {
            this.Show = false;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }
    }
</script>

<style scoped>
    .panel {
        width: 25%;
        margin: auto;
    }

    .card {
        position: absolute;
        width: 25%;
        top: 50%;
        transform: translateY(-50%);
    }

    h1 {
        text-align: center;
    }
</style>