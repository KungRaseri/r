<template>
    <v-container
        class="message"
    >
        <v-row
            v-for="item in getHistory"
            :key="item.color"
        >
            <Bubble
                :ColorFill="item.color"
                :MessageText="item.message" />
        </v-row>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import Bubble from './Bubble.vue';

    @Component({
        components: {
            Bubble
        },
    })
    export default class MessageBox extends Vue {
        private History: any[] = [];

        mounted() {
            this.getHistory = { color: "blue", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." };
            this.getHistory = { color: "red", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Cras semper auctor neque vitae. At in tellus integer feugiat. Morbi tincidunt ornare massa eget egestas purus. Faucibus vitae aliquet nec ullamcorper." };
            this.getHistory = { color: "green", message: "diam sit amet nisl suscipit" }
            this.getHistory = { color: "blue", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Massa sapien faucibus et molestie ac feugiat sed lectus." }
        }

        get getHistory() {
            return this.History;
        }

        set getHistory(value: any) {
            this.History.push(value);
        }
    }
</script>

<style scoped>
    .message {
        margin-top: 0.2%;
        margin-left: 0.2%;
        height: 50%;
        width: 30%
    }
</style>