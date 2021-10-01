#version 330 core
out vec4 FragColor;

in vec3 FragPos;  
in vec2 TexCoords;

uniform sampler2D texture1;
uniform vec3 viewPos;

void main()
{    
    float divisions = 100.0;
    float thickness = 0.02;
    float delta = 0.05 / 2.0;
    float cutoff = 3.0;

    float x = fract(TexCoords.x * divisions);
    x = min(x, 1.0 - x);

    float xdelta = fwidth(x);
    x = smoothstep(x - xdelta, x + xdelta, thickness);

    float y = fract(TexCoords.y * divisions);
    y = min(y, 1.0 - y);

    float ydelta = fwidth(y);
    y = smoothstep(y - ydelta, y + ydelta, thickness);

    float c =clamp(x + y, 0.0, 1.0);

    float dist = distance(FragPos, viewPos);
    vec3 color = vec3(0.0, c*0.5, c);
    if( dist < cutoff ) 
        FragColor = vec4(color, 1.0);
    else   
        FragColor = vec4(color / (dist + 1 - cutoff), 1.0 / (dist + 1 - cutoff));
}