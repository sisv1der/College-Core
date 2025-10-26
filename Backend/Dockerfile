FROM eclipse-temurin:25-jre-jammy

LABEL authors="Yarig"

WORKDIR /app
COPY target/*.jar app.jar
EXPOSE 8080

ENTRYPOINT ["java", "-jar", "app.jar"]