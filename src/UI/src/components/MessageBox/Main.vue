<template>
    <v-container
        class="message"
    >
        <v-row
            v-for="(item, index) in getHistory"
            :key="index"
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
            window.addEventListener("message", (e) => {
                console.log(e.data);
                switch (e.data.eventName) {
                    case "ADD_MESSAGE":
                        this.getHistory = e.data.messageData;
                        break;
                    default:
                        break;
                }
            });

            console.log("MessageBox mounted.");
        }

        updated() {
            this.$el.scrollTop = this.$el.scrollHeight;
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
        overflow: hidden;
        margin-top: 0.2%;
        margin-left: 0.2%;
        height: 50vh;
        width: 30%
    }
</style>